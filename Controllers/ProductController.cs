using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineShop.Hubs;
using OnlineShop.Interfaces;
using OnlineShop.Models;
using OnlineShop.Models.ApiViewModels;
using OnlineShop.Models.DB;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IHubContext<ProductHub, IProductHub> _hubProduct;

        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="provider"></param>
        public ProductController(IServiceProvider provider)
        {
            _logger = provider.GetRequiredService<ILogger<ProductController>>();
            _productService = provider.GetRequiredService<IProductService>();
            _hubProduct = provider.GetRequiredService<IHubContext<ProductHub, IProductHub>>();
        }

        /// <summary>
        /// 取得所有商品
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ResponseModel<List<Product>> GetProducts()
        {
            return _productService.GetProducts();
        }

        /// <summary>
        /// 新增上架商品
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("LaunchProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseModel<string>> LaunchProduct(ProductVM product)
        {
            return await _productService.LaunchProduct(product);
        }

        /// <summary>
        /// 修改庫存、價格等商品資訊
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("EditProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseModel<string>> EditProduct(ProductVM product)
        {
            var result = await _productService.EditProduct(product);

            // 通知用戶商品售價異動
            await _hubProduct.Clients.All.NotifyPriceChange(Convert.ToInt32(product.Id), Convert.ToString(product.SalesPrice));
            return result;
        }

        /// <summary>
        /// 商品下架
        /// </summary>
        /// <param name="pId">商品ID</param>
        /// <returns></returns>
        [HttpGet,Route("ReCallProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseModel<string>> ReCallProduct(int pId)
        {
            var result = await _productService.ReCallProduct(pId);

            // 通知用戶商品下架
            await _hubProduct.Clients.All.NotifyProductRecall(pId);
            return result;
        }
    }
}
