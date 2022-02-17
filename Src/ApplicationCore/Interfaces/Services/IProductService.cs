using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IProductService
    {
        Task AddProduct(string userId, string productName, string description, decimal price, int? colorId, int conditionId, int catId, int? brandId, bool offerable, bool issold, string picUri);

        Task<Product> GetById(int id);
        Task DeleteProduct(int productId);

        Task<bool>BuyProduct(int productId, decimal price);
        Task UpdateProduct(int productId , string productName, string description, decimal price, int? colorId, int conditionId, int catId, int? brandId, bool offerable, bool issold, string picUri);

        Task<List<Product>> GetAllProduct();
        Task<List<Product>> GetAllBuyableProduct();

        
    }
}
