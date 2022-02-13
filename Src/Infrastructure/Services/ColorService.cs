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
    public class ColorService : IColorService
    {
        private readonly IAsyncRepository<Color> _colorRepository;

        public ColorService(IAsyncRepository<Color> colorRepository)
        {
            _colorRepository = colorRepository;
        }
        public async Task<List<Color>> GetAllColor()
        {
            List<Color> colors = await _colorRepository.GetAllAsync();

            return colors;
        }

        public Task AddColor(string colorName)
        {
            Color color = new()
            {
                ColorName = colorName
            };
            _colorRepository.AddAsync(color);
            return Task.FromResult(color);
        }

        public async Task DeleteColor(int colorId)
        {
            Color color = await _colorRepository.GetByIdAsync(colorId);

           _colorRepository.DeleteAsync(color);

        }

    }
}
