using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models.DB;
using OnlineShop.Repository;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(IServiceProvider provider)
        {
            _logger = provider.GetRequiredService<ILogger<ProductController>>();
            _productService = provider.GetRequiredService<IProductService>();
        }

        /// <summary>
        /// 取得所有商品
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetProducts")]
        public List<Product> GetProducts()
        {
            //_logger.LogInformation($"測試Logger:{DateTime.Now}");
            return _productService.GetProducts();
        }
    }
}
