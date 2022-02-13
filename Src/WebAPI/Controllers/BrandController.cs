using ApplicationCore.Entities;
using ApplicationCore.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs.BrandDtos;

namespace WebAPI.Controllers
{
    [Route("api/v1/Brands")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //You can use the HttpGet request to take all Brand list
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.BrandService.GetAllBrands());
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDto brandDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            await _unitOfWork.BrandService.AddBrand(brandDto.BrandName);
            _unitOfWork.Complete();

            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
           await _unitOfWork.BrandService.DeleteBrand(id);
           _unitOfWork.Complete();
            return NoContent();
        }


    }
}
