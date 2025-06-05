﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.ReviewDtos
{
    public record ReviewUpdateDto
    {
        [Range(1, 5)]
        public int? Rating { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }
    }
}
