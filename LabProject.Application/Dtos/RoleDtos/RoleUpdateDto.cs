﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.RoleDtos
{
    public record RoleUpdateDto
    {
        [MaxLength(50)]
        public string? Name { get; set; }
    }
}
