using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.RoleDtos
{
    public record RoleCreateDto
    {
        [Required, MaxLength(50)]
        required public string Name { get; set; }
    }
}
