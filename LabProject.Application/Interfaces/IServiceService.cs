using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.ServiceDtos;
using LabProject.Domain.Entities;

namespace LabProject.Application.Interfaces
{
    public interface IServiceService : IBaseService<Service, ServiceDto, ServiceCreateDto, ServiceUpdateDto>
    {
        public Task<IEnumerable<ServiceDto>> GetServicesByProviderAsync(long providerId);
    }
}
