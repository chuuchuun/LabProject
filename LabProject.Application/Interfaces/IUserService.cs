using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Domain.Entities;

namespace LabProject.Application.Interfaces
{
    public interface IUserService : ICreatableService<UserCreateDto>
    {
        Task<string> LoginUser(UserLoginDto userLoginDto);
    }
}
