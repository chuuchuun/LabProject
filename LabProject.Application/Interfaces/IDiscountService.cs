using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Entities;

namespace LabProject.Application.Interfaces
{
    public interface IDiscountService : IBaseService<Discount>
    {
        public Task<IEnumerable<Discount>> GetValidDiscountsForClientAsync(long clientId);
    }
}
