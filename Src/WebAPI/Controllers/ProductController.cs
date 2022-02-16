﻿using ApplicationCore.Entities;
using ApplicationCore.Interfaces.UnitOfWork;
using IdentityModel;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.DTOs.ProductDtos;


namespace WebAPI.Controllers
{
    [Route("api/v1/Products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            string Roletype = User.FindFirstValue("Roletype");
            if (Roletype == "False")
            {
                return Ok(await _unitOfWork.ProductService.GetAllProduct());
            }
            return Unauthorized();

        }

        [HttpGet]
        [Authorize]
        [Route("OnSale")]
        public async Task<IActionResult> GetAllBuyableProduct()
        {
                return Ok(await _unitOfWork.ProductService.GetAllBuyableProduct());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _unitOfWork.ProductService.GetById(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto pDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            string userId = User.FindFirstValue("Id");
            await _unitOfWork.ProductService.AddProduct(userId, pDto.Name, pDto.Description, pDto.Price, pDto.ColorId, pDto.ConditionsOfProductId, pDto.CategoryId, pDto.BrandId, pDto.IsOfferable, pDto.IsSold, pDto.PictureUri);
            _unitOfWork.Complete();

            return Ok();
        }

        [Authorize]
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto upDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            await _unitOfWork.ProductService.UpdateProduct(upDto.Id, upDto.Name, upDto.Description, upDto.Price, upDto.ColorId, upDto.ConditionsOfProductId, upDto.CategoryId, upDto.BrandId, upDto.IsOfferable, upDto.IsSold, upDto.PictureUri);
            _unitOfWork.Complete();

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _unitOfWork.ProductService.DeleteProduct(id);
            _unitOfWork.Complete();
            return NoContent();
        }


        [Authorize]
        [Route("Buy")]
        [HttpPut]
        public async Task<IActionResult> BuyProduct([FromBody] BuyProductDto BuyDto)
        {

            string message1 = "Purchase operation is unsucces";
            string message2 = "Purchase operation is succes";

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            bool result = await _unitOfWork.ProductService.UpdateProduct(BuyDto.ProductId, BuyDto.Price);

            if (result)
            {
                return Ok(message2);
                _unitOfWork.Complete();
            }
            else
            {
                return BadRequest(message1);
            }

        }


    }
}
