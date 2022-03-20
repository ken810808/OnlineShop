using OnlineShop.Models.DB;
using OnlineShop.Repository;

namespace OnlineShop.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }

    public class ProductService: IProductService
    {
        private readonly Lazy<IProductRepository> _productRepository;

        public ProductService(IServiceProvider provider)
        {
            Console.WriteLine($"{DateTime.Now} ProductService Created!!!");
            _productRepository = provider.GetRequiredService<Lazy<IProductRepository>>(); ;
        }

        /// <summary>
        /// 取得商品
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Product> GetProducts()
        {
            return _productRepository.Value.GetAsNotracking(x => x.Status == 1);
        }
    }
}
