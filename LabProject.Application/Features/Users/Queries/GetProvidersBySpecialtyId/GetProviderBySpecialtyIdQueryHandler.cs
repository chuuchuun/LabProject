using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Domain.Interfaces;
using MediatR;

namespace LabProject.Application.Features.Users.Queries.GetProvidersBySpecialtyId
{
    public class GetProviderBySpecialtyIdQueryHandler(IUserRepository userRepo, IMapper mapper)
          : IRequestHandler<GetProviderBySpecialtyId, IEnumerable<UserProviderDto?>>
    {
        private readonly IUserRepository _userRepo = userRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<UserProviderDto?>> Handle(GetProviderBySpecialtyId request, CancellationToken cancellationToken)
        {
            var providers = await _userRepo.GetProvidersBySpecialtyAsync(request.Id);
            return _mapper.Map<IEnumerable<UserProviderDto>>(providers);
        }
    }
}
