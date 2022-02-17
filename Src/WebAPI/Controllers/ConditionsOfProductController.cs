using ApplicationCore.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.DTOs.ConditionsOfProductDtos;

namespace WebAPI.Controllers
{
    [Route("api/v1/Conditions")]
    [ApiController]
    [Authorize]
    public class ConditionsOfProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConditionsOfProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.ConditionsOfProductService.GetAllConditions());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCondition([FromBody] CreateConditionDto conditionDto)
        {

            string Roletype = User.FindFirstValue("Roletype");
            if (Roletype == "False")
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                await _unitOfWork.ConditionsOfProductService.AddCondition(conditionDto.Condition);
                _unitOfWork.Complete();
                return Ok();
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCondition(int id)
        {
            string Roletype = User.FindFirstValue("Roletype");
            if (Roletype == "False")
            {
                await _unitOfWork.ConditionsOfProductService.DeleteCondition(id);
                _unitOfWork.Complete();
                return NoContent();
            }
            return Unauthorized();
        }

    }
}
