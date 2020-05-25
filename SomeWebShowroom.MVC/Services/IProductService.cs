using SomeWebShowroom.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SomeWebShowroom.MVC.Services
{
    public interface IProductService
    {
        public Task Create(Product product);

        public Task<IEnumerable<Product>> GettAll();

        public Task<Product> GetProduct(int id);

        public Task Update(Product product);

        public Task Delete(int id);

        Task AddTooFavoriteList(string userId, int productId);
        Task RemoveFromFavoriteList(string userId, int productId);

        Task<bool> IsproductInFavoriteList(string userId, int productId);
        Task<IEnumerable<Product>> GetFavoriteListByUser(string id);
    }
}
