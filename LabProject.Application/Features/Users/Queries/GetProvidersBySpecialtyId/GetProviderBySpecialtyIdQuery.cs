using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.UserDtos;
using MediatR;

namespace LabProject.Application.Features.Users.Queries.GetProvidersBySpecialtyId
{
    public record GetProviderBySpecialtyId(long Id) : IRequest<IEnumerable<UserProviderDto?>>;
}
