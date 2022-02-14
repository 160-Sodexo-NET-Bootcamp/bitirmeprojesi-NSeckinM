using ApplicationCore.Interfaces.Services;
using ApplicationCore.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ApplicationDbContext _dbContext;
        public IBrandService BrandService { get; }
        public ICategoryService CategoryService { get; }
        public IColorService ColorService { get; }
        public IConditionsOfProductService ConditionsOfProductService { get; }
        public IOfferService OfferService { get; }
        public IProductService ProductService { get; }
        public IMailService MailService { get; }

        public UnitOfWork(ApplicationDbContext dbContext, IBrandService brandService, ICategoryService categoryService, IColorService colorService, IConditionsOfProductService conditionsOfProductService, IOfferService offerService, IProductService productService,IMailService mailService )
        {
            _dbContext = dbContext;
            BrandService = brandService;
            CategoryService = categoryService;
            ColorService = colorService;
            ConditionsOfProductService = conditionsOfProductService;
            OfferService = offerService;
            ProductService = productService;
            MailService = mailService;
        }
        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
