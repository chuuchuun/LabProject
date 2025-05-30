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
    public class DiscountService(IRepository<Discount> discountRepo) : IBaseService<Discount>
    {
        private readonly IRepository<Discount> _discountRepo = discountRepo;

        public async Task<long> AddAsync(Discount entityModel)
        {
            return await _discountRepo.AddAsync(entityModel);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _discountRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            return await _discountRepo.GetAllAsync();
        }

        public async Task<Discount?> GetByIdAsync(long id)
        {
            return await _discountRepo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(long id, Discount entityModel)
        {
            return await _discountRepo.UpdateAsync(id, entityModel);
        }
    }
}
