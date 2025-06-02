using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Features.Users.Commands.CreateUser;
using LabProject.Domain.Interfaces;
using MediatR;

namespace LabProject.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserHandler(IUserRepository userRepo, IMapper mapper) : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepo = userRepo;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepo.GetByIdAsync(request.Id);
            if (existingUser is null)
                return false;

            _mapper.Map(request.Dto, existingUser);
            existingUser.UpdatedAt = DateTime.UtcNow;

            return await _userRepo.UpdateAsync(request.Id, existingUser);
        }
    }

}
