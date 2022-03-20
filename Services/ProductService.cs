using OnlineShop.Enums;
using OnlineShop.Models;
using OnlineShop.Models.ApiViewModels;
using OnlineShop.Models.DB;
using OnlineShop.Repository;

namespace OnlineShop.Services
{
    public interface IProductService
    {
        /// <summary>
        /// 取得商品
        /// </summary>
        /// <returns></returns>
        ResponseModel<List<Product>> GetProducts();

        /// <summary>
        /// 新增上架商品
        /// </summary>
        /// <returns></returns>
        Task<ResponseModel<string>> LaunchProduct(ProductVM product);

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <returns></returns>
        Task<ResponseModel<string>> EditProduct(ProductVM product);

        /// <summary>
        /// 商品下架
        /// </summary>
        /// <param name="pId">商品ID</param>
        /// <returns></returns>
        Task<ResponseModel<string>> ReCallProduct(int pId);
    }

    public class ProductService: IProductService
    {
        private readonly Lazy<IProductRepository> _productRepository;

        public ProductService(IServiceProvider provider)
        {
            Console.WriteLine($"{DateTime.Now} ProductService Created!!!");
            _productRepository = provider.GetRequiredService<Lazy<IProductRepository>>();
        }

        /// <summary>
        /// 取得商品
        /// </summary>
        /// <returns></returns>
        public ResponseModel<List<Product>> GetProducts()
        {
            var respModel = new ResponseModel<List<Product>>();

            respModel.Success = true;
            respModel.Data = _productRepository.Value.GetAsNotracking(x => x.Status == 1);
            return respModel;
        }

        /// <summary>
        /// 添加上架商品
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseModel<string>> LaunchProduct(ProductVM product)
        {
            var respModel=new ResponseModel<string>();

            #region 欄位檢查及轉換成資料表模型
            var newProduct = new Product();
            // ...略
            #endregion
            await _productRepository.Value.Save(newProduct);
            respModel.Success = true;
            respModel.Message="商品上架成功!!";
            return respModel;
        }

        /// <summary>
        /// 修改庫存、價格等商品資訊
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<ResponseModel<string>> EditProduct(ProductVM productVm)
        {
            var respModel=new ResponseModel<string>();
            var product = _productRepository.Value.Find((long)Convert.ToInt32(productVm.Id));
            // 檢查及轉換模型
            await _productRepository.Value.UpdateOnlyColumn(product, p => new
            {
                p.Status
                // 略...
            });
            respModel.Success = true;
            respModel.Message="商品修改成功!!";
            return respModel;
        }

        /// <summary>
        /// 商品下架
        /// </summary>
        /// <param name="pId">商品ID</param>
        /// <returns></returns>
        public async Task<ResponseModel<string>> ReCallProduct(int pId)
        {
            var respModel = new ResponseModel<string>();
            var product = _productRepository.Value.Find((long)pId);
            product.Status = (sbyte)ProductStatusEnum.ReCall;
            await _productRepository.Value.UpdateOnlyColumn(product, p => new
            {
                p.Status
            });
            respModel.Success = true;
            respModel.Message = "商品已下架!!";
            return respModel;
        }
    }
}
