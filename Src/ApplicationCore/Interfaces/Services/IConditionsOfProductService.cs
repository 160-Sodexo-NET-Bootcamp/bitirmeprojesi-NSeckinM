using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IConditionsOfProductService
    {
        Task AddCondition(string condition);

        Task DeleteCondition(int conditionId);

        Task<List<ConditionsOfProduct>> GetAllConditions();

    }
}
