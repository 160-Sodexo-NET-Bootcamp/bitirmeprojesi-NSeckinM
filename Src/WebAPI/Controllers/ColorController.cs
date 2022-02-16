using ApplicationCore.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.DTOs.ColorDtos;

namespace WebAPI.Controllers
{
    [Route("api/v1/Colors")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ColorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.ColorService.GetAllColor());
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateColor([FromBody] CreateColorDto ColorDto)
        {
            string Roletype = User.FindFirstValue("Roletype");
            if (Roletype == "False")
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }
                await _unitOfWork.ColorService.AddColor(ColorDto.ColorName);
                _unitOfWork.Complete();
                return Ok();
            }
            return Unauthorized();
            
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            string Roletype = User.FindFirstValue("Roletype");
            if (Roletype == "False")
            {
                await _unitOfWork.ColorService.DeleteColor(id);
                _unitOfWork.Complete();
                return NoContent();
            }
            return Unauthorized();

        }

    }
}
