﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.CategoryDtos
{
    public class CreateCategoryDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
