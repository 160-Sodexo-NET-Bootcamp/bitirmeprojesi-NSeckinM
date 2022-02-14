using ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBrandService BrandService { get; }
        ICategoryService CategoryService { get; }
        IColorService ColorService { get; }
        IConditionsOfProductService ConditionsOfProductService { get; }
        IOfferService OfferService { get; }
        IProductService ProductService { get; }
        IMailService MailService { get; }
        int Complete();

    }
}
