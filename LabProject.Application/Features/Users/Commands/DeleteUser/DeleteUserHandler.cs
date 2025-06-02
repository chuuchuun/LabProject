using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Features.Users.Commands.UpdateUser;
using LabProject.Domain.Interfaces;
using MediatR;

namespace LabProject.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserHandler(IUserRepository userRepo) : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepo = userRepo;

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepo.DeleteAsync(request.Id);
        }
    }
}
