using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IColorService
    {

        Task<List<Color>> GetAllColor();
        Task AddColor(string colorName);
        Task DeleteColor(int colorId);


    }
}
