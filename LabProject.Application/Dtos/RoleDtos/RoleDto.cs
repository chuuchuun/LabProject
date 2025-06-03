using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.RoleDtos
{
    public class RoleDto : BaseDto
    {
        public int Id { get; set; }
        required public string Name { get; set; }
    }

}
