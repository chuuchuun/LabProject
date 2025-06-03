using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Domain.Interfaces;
using MediatR;

namespace LabProject.Application.Features.Users.Queries
{
    public record GetUserByIdQuery(long Id) : IRequest<UserDto?>;

    public class GetUserByIdQueryHandler(IUserRepository userRepo, IMapper mapper)
          : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepo.GetByIdAsync(request.Id);
            return user is null ? null : mapper.Map<UserDto>(user);
        }
    }
}
