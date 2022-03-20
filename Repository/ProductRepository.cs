using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.DB;

namespace OnlineShop.Repository
{
    public interface IProductRepository: IBaseRepository<Product>
    {
    }

    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        protected online_shopContext _onlineShopContext;
        public ProductRepository(online_shopContext context) : base(context)
        {
            Console.WriteLine($"{DateTime.Now} ProductRepository Created!!!");
            _onlineShopContext = context;
        }

       
    }
}
