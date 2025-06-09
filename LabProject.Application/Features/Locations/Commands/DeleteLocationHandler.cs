using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using MediatR;

namespace LabProject.Application.Features.Locations.Commands
{
    public record  DeleteLocationCommand(long Id) : IRequest<bool>;
    
    public class DeleteLocationHandler(IRepository<Location> repository) : IRequestHandler<DeleteLocationCommand, bool>
    {
        private readonly IRepository<Location> _repository = repository;
        public async Task<bool> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.Id);
        }
    }
}
