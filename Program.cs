using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.DB;
using NLog.Web;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Net.Mime;
using HealthChecks.UI.Client;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using Autofac;
using OnlineShop.Hubs;
using OnlineShop;

var builder = WebApplication.CreateBuilder(args);

#region 使用Autofac容器

// === 1. 註冊服務 ===
//取得目前執行App的Assembly
Assembly assembly = Assembly.GetExecutingAssembly();

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((container) => {

        // 註冊所有名稱為Service及Repository結尾的物件
        container.RegisterAssemblyTypes(assembly)
        .Where(t => t.Name.EndsWith("Service"))
        .AsImplementedInterfaces()
        .AutoActivate().SingleInstance();

        container.RegisterAssemblyTypes(assembly)
        .Where(t => t.Name.EndsWith("Repository"))
        .AsImplementedInterfaces()
        .AutoActivate().SingleInstance();

        container.RegisterAssemblyTypes(assembly)
       .Where(t => t.Name.EndsWith("Hub"))
       .AsImplementedInterfaces()
       .AutoActivate().SingleInstance();
    });

#endregion

// 取得appsettings.json資訊
var config = builder.Configuration.GetConnectionString("OnlineShop");

// Refistering DB
builder.Services.AddDbContext<online_shopContext>(option => option.UseMySql(
    config,
    ServerVersion.AutoDetect(config)));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register HealthChecks Service
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

// Register SignalR Service
builder.Services.AddSignalR();

#region HealthChecks

builder.WebHost.ConfigureLogging(logging => {
    logging.ClearProviders();
    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
}).UseNLog();

builder.Services.AddHealthChecks()
    .AddUrlGroup(new Uri("https://www.google.com"), "Third party api health check", HealthStatus.Unhealthy);

#endregion

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();

#region Use HealthChecks

app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = async (context, report) => {
        var result = JsonConvert.SerializeObject(
            new
            {
                status = report.Status.ToString(),
                errors = report.Entries.Select(e =>
                new
                {
                    key = e.Key,
                    value = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                })
            }); 
        context.Response.ContentType = MediaTypeNames.Application.Json; 
        await context.Response.WriteAsync(result);
    }
});

app.UseHealthChecks("/hc",
    new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.UseHealthChecksUI(options => { options.UIPath = "/hc-ui"; });

#endregion

#region Use SignalR

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ProductHub>("/hub");
});

#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
