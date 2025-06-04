using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.TokenDtos
{
    public class TokenDto
    {
        required public string AccessToken { get; set; }
        required public string RefreshToken { get; set; }
    }
}
