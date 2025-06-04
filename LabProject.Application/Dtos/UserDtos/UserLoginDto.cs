using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.UserDtos
{
    public class UserLoginDto
    {
        required public string Username { get; set; }
        required public string Password { get; set; }
    }
}
