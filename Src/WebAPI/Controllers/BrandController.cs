using ApplicationCore.Entities;
using ApplicationCore.Interfaces.UnitOfWork;
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

        //You can use the HttpGet request to take all Container list
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.BrandService.GetAllBrands());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDto BrandDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            await _unitOfWork.BrandService.AddBrand(BrandDto.BrandName);
            _unitOfWork.Complete();

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
           await _unitOfWork.BrandService.DeleteBrand(id);
           _unitOfWork.Complete();
            return NoContent();
        }


    }
}
