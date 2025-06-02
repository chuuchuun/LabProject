using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Domain.Interfaces;
using MediatR;

namespace LabProject.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler(IUserRepository userRepo, IMapper mapper)
        : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepo.GetAllAsync();
            return mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
