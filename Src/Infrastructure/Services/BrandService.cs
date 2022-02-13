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
    public class BrandService : IBrandService
    {
        private readonly IAsyncRepository<Brand> _brandRepository;

        public BrandService(IAsyncRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public Task AddBrand(string brandName)
        {
            Brand brand = new()
            {
                BrandName = brandName
            };
            _brandRepository.AddAsync(brand);

            return Task.FromResult(brand);
        }

        public async Task DeleteBrand(int brandId)
        {
            Brand brand = await _brandRepository.GetByIdAsync(brandId);
            _brandRepository.DeleteAsync(brand);
        }

        public async Task<List<Brand>> GetAllBrands()
        {
            List<Brand> brands = await _brandRepository.GetAllAsync();
            return brands;
        }
    }
}
