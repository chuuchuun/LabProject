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
    public class PaymentService(IRepository<Payment> paymentRepo) : IBaseService<Payment>
    {
        private readonly IRepository<Payment> _paymentRepo = paymentRepo;

        public async Task<long> AddAsync(Payment entityModel)
        {
            return await _paymentRepo.AddAsync(entityModel);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _paymentRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _paymentRepo.GetAllAsync();
        }

        public async Task<Payment?> GetByIdAsync(long id)
        {
            return await _paymentRepo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(long id, Payment entityModel)
        {
            return await _paymentRepo.UpdateAsync(id, entityModel);
        }
    }
}
