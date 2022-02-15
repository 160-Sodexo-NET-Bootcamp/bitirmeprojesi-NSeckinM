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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.ProductService.GetAllProduct());
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _unitOfWork.ProductService.GetById(id));
        }

        [HttpPost]
       // [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto pDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _unitOfWork.ProductService.AddProduct(userId,pDto.Name,pDto.Description,pDto.Price,pDto.ColorId,pDto.ConditionsOfProductId,pDto.CategoryId,pDto.BrandId,pDto.IsOfferable,pDto.IsSold,pDto.PictureUri);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpPost]
        [Route("Update")]
        // [Authorize(Roles = "admin")]
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

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _unitOfWork.ProductService.DeleteProduct(id);
            _unitOfWork.Complete();
            return NoContent();
        }


    }
}
