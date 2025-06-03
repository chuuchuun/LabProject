using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.DiscountDtos;
using LabProject.Domain.Entities;

namespace LabProject.Application.Interfaces
{
    public interface IDiscountService : ICreatableService<DiscountCreateDto>,IReadableService<DiscountDto>, IUpdatableService<DiscountUpdateDto>, IDeletableService
    {
        public Task<IEnumerable<DiscountDto>> GetValidDiscountsForClientAsync(long clientId);
    }
}
