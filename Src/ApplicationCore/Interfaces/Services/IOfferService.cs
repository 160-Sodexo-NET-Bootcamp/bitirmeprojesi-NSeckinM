using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IOfferService
    {

        Task AddOffer(string userId, int percentageOfOffer, int productId);

        Task DeleteOffer(int offerId);

        Task<Offer> GetByIdOffer(int id);

        //Sadece Admin Sistemdeki bütün offerları görebilmeli
        Task<List<Offer>> GetAllOffer();
        Task<List<Product>> GetAllReceivedOfferOfUsers(string userId);
        Task<List<Offer>> GetAllSendedOfferOfUsers(string userId);



    }
}
