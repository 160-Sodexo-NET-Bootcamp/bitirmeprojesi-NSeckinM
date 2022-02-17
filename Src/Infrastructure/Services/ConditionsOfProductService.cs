using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class ConditionsOfProductService : IConditionsOfProductService
    {
        private readonly IAsyncRepository<ConditionsOfProduct> _conditionRepository;

        public ConditionsOfProductService(IAsyncRepository<ConditionsOfProduct> conditionRepository)
        {
            _conditionRepository = conditionRepository;
        }
        public Task AddCondition(string condition)
        {
            ConditionsOfProduct productsCondition = new()
            {
                Condition = condition
            };
            _conditionRepository.AddAsync(productsCondition);
            return Task.FromResult(productsCondition);
        }

        public async Task DeleteCondition(int conditionId)
        {
                ConditionsOfProduct productsCondition = await _conditionRepository.GetByIdAsync(conditionId);
                _conditionRepository.DeleteAsync(productsCondition);
        }

        public async Task<List<ConditionsOfProduct>> GetAllConditions()
        {
            List<ConditionsOfProduct> conditionsOfProducts = await _conditionRepository.GetAllAsync();
            return conditionsOfProducts;
        }
    }
}
