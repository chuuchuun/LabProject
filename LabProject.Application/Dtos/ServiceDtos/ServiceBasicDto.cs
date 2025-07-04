﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.ServiceDtos
{
    public record ServiceBasicDto
    {
        public long Id { get; set; }
        required public string Name { get; set; }
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
    }
}
