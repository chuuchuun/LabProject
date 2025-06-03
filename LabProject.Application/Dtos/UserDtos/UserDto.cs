using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.RoleDtos;

namespace LabProject.Application.Dtos.UserDtos
{
    public class UserDto : BaseDto
    {
        public long Id { get; set; }
        required public string Name { get; set; }
        required public string Username { get; set; }
        required public string Phone { get; set; }
        required public string Email { get; set; }
        public int RoleId { get; set; }
        required public RoleDto Role { get; set; }
    }
}
