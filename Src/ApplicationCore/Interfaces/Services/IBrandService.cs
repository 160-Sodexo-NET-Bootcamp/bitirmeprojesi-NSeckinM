using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IBrandService
    {

        Task AddBrand(string brandName);

        Task DeleteBrand(int brandId);

        Task<List<Brand>> GetAllBrands();

    }
}
