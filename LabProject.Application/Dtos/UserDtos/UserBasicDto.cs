using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.UserDtos
{
    public class UserBasicDto
    {
        public long Id { get; set; }
        required public string Name { get; set; }
    }
}
