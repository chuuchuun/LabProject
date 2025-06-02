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
        public string Name { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public RoleDto Role { get; set; }
    }
}
