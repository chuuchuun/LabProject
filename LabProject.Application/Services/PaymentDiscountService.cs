using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class PaymentDiscountService(IRepository<PaymentDiscount> paymentDiscountRepo) : IBaseService<PaymentDiscount>
    {
        private readonly IRepository<PaymentDiscount> _paymentDiscountRepo = paymentDiscountRepo;
        public async Task<long> AddAsync(PaymentDiscount entityModel)
        {
            return await _paymentDiscountRepo.AddAsync(entityModel);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _paymentDiscountRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<PaymentDiscount>> GetAllAsync()
        {
            return await _paymentDiscountRepo.GetAllAsync();
        }

        public async Task<PaymentDiscount?> GetByIdAsync(long id)
        {
            return await _paymentDiscountRepo.GetByIdAsync(id);
        }

        public Task<bool> UpdateAsync(long id, PaymentDiscount entityModel)
        {
            throw new NotImplementedException();
        }
    }
}
