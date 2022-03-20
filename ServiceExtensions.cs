using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using OnlineShop.Models;

namespace OnlineShop
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 自定義錯誤捕捉返回內容
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var err = new ResponseModel<string>
                        {
                            Success = false,
                            Code = Convert.ToString(context.Response.StatusCode),
                            Message = "發生未預期錯誤"
                        };
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(err));
                    }
                });
            });
        }
    }
}
