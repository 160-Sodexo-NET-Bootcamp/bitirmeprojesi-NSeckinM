using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAsyncRepository<Offer> _offerRepository;

        public OfferService(ApplicationDbContext dbContext, IAsyncRepository<Offer> offerRepository)
        {
            _dbContext = dbContext;
            _offerRepository = offerRepository;
        }

        public Task AddOffer(string userId, int percentageOfOffer, int productId)
        {
            Offer offer = new()
            {
                UserId = userId,
                ProductId = productId,
                PercentageOfOffer = percentageOfOffer,
            };
            _offerRepository.AddAsync(offer);
            return Task.FromResult(offer);
        }

        public async Task DeleteOffer(int offerId)
        {
            Offer offer = await _offerRepository.GetByIdAsync(offerId);
            _offerRepository.DeleteAsync(offer);
        }

        public Task<List<Offer>> GetAllOffer()
        {
            return _offerRepository.GetAllAsync();
        }

        public Task<List<Product>> GetAllReceivedOfferOfUsers(string userId)
        {
            List<Product> offersOfProduct = _dbContext.Products.Include(x => x.Offers).Where(x => x.UserId.Equals(userId)).ToList();
            return Task.FromResult(offersOfProduct);
        }

        public Task<List<Offer>> GetAllSendedOfferOfUsers(string userId)
        {
            List<Offer> sendedoffers = _dbContext.Offers.Include(x => x.Product).Where(x => x.UserId.Equals(userId)).ToList();
            return Task.FromResult(sendedoffers);
        }

        public async Task<Offer> GetByIdOffer(int id)
        {
            Offer offer = await _offerRepository.GetByIdAsync(id);
            return offer;
        }
    }
}
