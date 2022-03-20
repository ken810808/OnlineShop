using OnlineShop.Repository;

namespace OnlineShop.Services
{
    public interface IUserService
    { 
    }

    public class UserService: IUserService
    {
        private readonly Lazy<IProductRepository> _productRepository;

        public UserService(IServiceProvider provider)
        {
            Console.WriteLine($"{DateTime.Now} UserService Created!!!");
            _productRepository = provider.GetRequiredService<Lazy<IProductRepository>>(); ;
        }
    }
}
