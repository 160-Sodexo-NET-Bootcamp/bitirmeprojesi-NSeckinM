using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAsyncRepository<Product> _productRepository;

        public ProductService(ApplicationDbContext dbContext, IAsyncRepository<Product> productRepository)
        {
            _dbContext = dbContext;
            _productRepository = productRepository;
        }

        public Task AddProduct(string userId, string productName, string description, decimal price, int? colorId, int conditionId, int catId, int? brandId, bool offerable, bool issold, string picUri)
        {
            Product product = new()
            {
                UserId = userId,
                Name = productName,
                Description = description,
                Price = price,
                ColorId = colorId,
                ConditionsOfProductId = conditionId,
                CategoryId = catId,
                BrandId = brandId,
                IsOfferable = offerable,
                IsSold = issold,
                PictureUri = picUri
            };
            _productRepository.AddAsync(product);
            return Task.FromResult(product);
        }


        public async Task DeleteProduct(int productId)
        {
          Product product = await _productRepository.GetByIdAsync(productId);
            _productRepository.DeleteAsync(product);
        }

        public async Task<List<Product>> GetAllProduct()
        {
            List<Product> products =await _productRepository.GetAllAsync();
            return products;
        }

        public async Task<List<Product>> GetAllBuyableProduct()
        {
            List<Product> products = _dbContext.Products.Where(x => x.IsSold == false).ToList();
            return  products;
        }

        public async Task<Product> GetById(int id)
        {
            Product product = await _productRepository.GetByIdAsync(id);
            return product;
        }

        public async Task UpdateProduct(int productId, string productName, string description, decimal price, int? colorId, int conditionId, int catId, int? brandId, bool offerable, bool issold, string picUri)
        {
            Product product = await _productRepository.GetByIdAsync(productId);

            product.Name = productName;
            product.Description = description;
            product.Price = price;
            product.ColorId = colorId;
            product.ConditionsOfProductId = conditionId;
            product.CategoryId = catId;
            product.BrandId = brandId;
            product.IsOfferable = offerable;
            product.IsSold = issold;
            product.PictureUri = picUri;
             _productRepository.UpdateAsync(product);
        }

        public async Task<bool> UpdateProduct(int productId, decimal price)
        {
            Product product = await _productRepository.GetByIdAsync(productId);

            if (product.Price <= price && product.IsSold == false)
            {
                product.IsSold = true;
               await _productRepository.UpdateAsync(product);
                return true;
            }

            return false;
            
        }
    }
}
