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
        required public string Name { get; set; }

        required public string Username { get; set; }

        required public string Phone { get; set; }

        required public string Email { get; set; }

        required public string Password { get; set; }

        required public string RoleName { get; set; }
    }
}
