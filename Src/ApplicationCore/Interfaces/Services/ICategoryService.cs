using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface ICategoryService
    {

        Task AddCategory(string categoryName);

        Task<Category> GetByIdCategory(int id);

        Task DeleteCategory(int categoryId);

        Task<List<Category>> GetAllCategory();

    }
}
