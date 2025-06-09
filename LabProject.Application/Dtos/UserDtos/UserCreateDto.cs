using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.UserDtos
{
    public record UserCreateDto
    {
        [Required, MaxLength(100)]
        required public string Name { get; set; }

        [Required, MaxLength(100)]
        required public string Username { get; set; }

        [Required, MaxLength(20)]
        required public string Phone { get; set; }

        [Required, MaxLength(255), EmailAddress]
        required public string Email { get; set; }

        [Required, MaxLength(255)]
        required public string Password { get; set; }

        [Required]
        required public string RoleName { get; set; }
    }
}
