﻿using ApplicationCore.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.ColorService.GetAllColor());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] CreateColorDto ColorDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            await _unitOfWork.ColorService.AddColor(ColorDto.ColorName);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            await _unitOfWork.ColorService.DeleteColor(id);
            _unitOfWork.Complete();
            return NoContent();
        }

    }
}
