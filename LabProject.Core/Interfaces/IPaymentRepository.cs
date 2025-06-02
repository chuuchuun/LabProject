using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Entities;

namespace LabProject.Domain.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        public Task<decimal> GetTotalRevenueByProviderAsync(long providerId, DateTime startDate, DateTime endDate);

    }
}
