using ApplicationCore.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.DTOs.CategoryDtos;

/*            string Roletype = User.FindFirstValue("Roletype");
            if (Roletype == "False")
            {

            }
            return Unauthorized();
*/

namespace WebAPI.Controllers
{
    [Route("api/v1/Categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.CategoryService.GetAllCategory());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _unitOfWork.CategoryService.GetByIdCategory(id));
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {

            string Roletype = User.FindFirstValue("Roletype");
            if (Roletype == "False")
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                await _unitOfWork.CategoryService.AddCategory(categoryDto.CategoryName);
                _unitOfWork.Complete();

                return Ok();
            }
            return Unauthorized();

        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            string Roletype = User.FindFirstValue("Roletype");
            if (Roletype == "False")
            {
                await _unitOfWork.CategoryService.DeleteCategory(id);
                _unitOfWork.Complete();
                return NoContent();
            }
            return Unauthorized();
        }


    }
}
