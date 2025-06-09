using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Interfaces;
using MediatR;

namespace LabProject.Application.Features.Services.Commands
{
    public record DeleteServiceCommand(long Id) : IRequest<bool>;
    public class DeleteServiceHandler(IServiceRepository serviceRepository) : IRequestHandler<DeleteServiceCommand, bool>
    {
        private readonly IServiceRepository _serviceRepository = serviceRepository;
        public async Task<bool> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _serviceRepository.DeleteAsync(request.Id);
            }
            catch
            {
                return false;
            }
        }
    }
}
