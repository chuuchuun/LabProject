using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Domain.Entities;

namespace LabProject.Application.Interfaces
{
    public interface IUserService : IBaseService<User, UserDto,UserCreateDto, UserUpdateDto>
    {
        Task<IEnumerable<UserProviderDto>> GetProvidersBySpecialtyIdAsync(long specialtyId);
        Task<string> LoginUser(UserLoginDto userLoginDto);
    }
}
