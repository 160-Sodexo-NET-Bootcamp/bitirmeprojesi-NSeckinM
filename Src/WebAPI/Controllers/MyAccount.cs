using ApplicationCore.Entities;
using ApplicationCore.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.DTOs.OfferDtos;

namespace WebAPI.Controllers
{
    [Route("api/v1/MyAccount")]
    [ApiController]
    public class MyAccount : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MyAccount(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Authorize]
        [Route("SendedOffer")]
        public async Task<IActionResult> GetAllSendedOffers()
        {
            string userId = User.FindFirstValue("Id");
            return Ok(await _unitOfWork.OfferService.GetAllSendedOfferOfUsers(userId));
        }

        [HttpGet]
        [Authorize]
        [Route("RecivedOffer")]
        public async Task<IActionResult> GetAllRecivedOffers()
        {
            string userId = User.FindFirstValue("Id");
            List<Product> products = await _unitOfWork.OfferService.GetAllReceivedOfferOfUsers(userId);
            return Ok(products.Select(x => new RecivedOfferDto() { ProductId = x.Id, ProductName = x.Name, Offers = x.Offers }));
        }


        [HttpPut]
        [Authorize]
        [Route("RecivedOffer")]
        public async Task<IActionResult> ReplyOffer(ReplyOfferDto replyOfferDto)
        {
            Offer offer = await _unitOfWork.OfferService.GetByIdOffer(replyOfferDto.OfferId);

            offer.StatusOfOffer = replyOfferDto.Reply.ToString();
            _unitOfWork.Complete();

            return Ok();
        }


    }
}
