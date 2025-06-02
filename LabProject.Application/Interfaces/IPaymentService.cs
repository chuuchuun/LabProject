using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.PaymentDtos;
using LabProject.Domain.Entities;

namespace LabProject.Application.Interfaces
{
    public interface IPaymentService : ICreatableService<PaymentCreateDto>, IReadableService<PaymentDto>, IDeletableService
    {
        public Task<decimal> GetTotalRevenueByProviderAsync(long providerId, DateTime startDate, DateTime endDate);
    }
}
