using ApplicationCore.Entities;
using ApplicationCore.Enum;
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
    [Authorize]
    public class MyAccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MyAccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Route("SendedOffer")]
        public async Task<IActionResult> GetAllSendedOffers()
        {
            string userId = User.FindFirstValue("Id");
            return Ok(await _unitOfWork.OfferService.GetAllSendedOfferOfUsers(userId));
        }

        [HttpGet]
        [Route("RecivedOffer")]
        public async Task<IActionResult> GetAllRecivedOffers()
        {
            string userId = User.FindFirstValue("Id");
            List<Product> products = await _unitOfWork.OfferService.GetAllReceivedOfferOfUsers(userId);
            _unitOfWork.Complete();
            return Ok(products.Select(x => new RecivedOfferDto() { ProductId = x.Id, ProductName = x.Name, Offers = x.Offers }));
        }


        [HttpPut]
        [Route("ReplyOffer")]
        public async Task<IActionResult> ReplyOffer(ReplyOfferDto replyOfferDto)
        {
            Offer offer = await _unitOfWork.OfferService.GetByIdOffer(replyOfferDto.OfferId);

            if (replyOfferDto.Reply == OfferStatus.Onayla)
            {
                offer.StatusOfOffer = replyOfferDto.Reply.ToString();
                offer.Product.Price = offer.OfferedValue;
                _unitOfWork.Complete();
                return Ok("Approved");
            }
            else
            {
                offer.StatusOfOffer = replyOfferDto.Reply.ToString();
                _unitOfWork.Complete();
                return Ok("Rejected");
            }
        }

        [HttpPost]
        [Route("SendOffer")]
        public async Task<IActionResult> SendOffer(SendOfferDto sendOfferDto)
        {
            //Login olmuş user ın Id si Claimde tutulup token uzerinden alıyorum.
            string userId = User.FindFirstValue("Id");
            Product product = await _unitOfWork.ProductService.GetById(sendOfferDto.ProductId);
            decimal offeredvalue = product.Price - ((product.Price * sendOfferDto.PercentageOfOffer) / 100);
            if (product.IsOfferable)
            {
                await _unitOfWork.OfferService.AddOffer(userId, sendOfferDto.PercentageOfOffer, offeredvalue, sendOfferDto.ProductId);
                _unitOfWork.Complete();
                return Ok("Your offer Created Succesfully");

            }

            return BadRequest("This product is not able to receive offer due to product owner preference");


        }

        [HttpDelete]
        [Route("WithdrawOffer")]
        public async Task<IActionResult> WithdrawOffer(WithdrawOfferDto WithdrawOfferDto)
        {
            int count = 0;
            string userId = User.FindFirstValue("Id");
            List<Offer> UsersOffer = await _unitOfWork.OfferService.GetAllSendedOfferOfUsers(userId);
            foreach (var item in UsersOffer)
            {
                if (item.Id == WithdrawOfferDto.OfferId)
                {
                    await _unitOfWork.OfferService.DeleteOffer(WithdrawOfferDto.OfferId);
                    count++;

                }
            }
            if (count == 0)
            {
                return BadRequest("You do not have any offer with this OfferId");
            }
            else
            {
                _unitOfWork.Complete();
                return Ok();
            }
        }

    }
}
