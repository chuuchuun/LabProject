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
    public class ReviewService(IRepository<Review> reviewRepo) : IBaseService<Review>
    {
        private readonly IRepository<Review> _reviewRepo = reviewRepo;

        public async Task<long> AddAsync(Review entityModel)
        {
            return await _reviewRepo.AddAsync(entityModel);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _reviewRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _reviewRepo.GetAllAsync();
        }

        public async Task<Review?> GetByIdAsync(long id)
        {
            return await _reviewRepo.GetByIdAsync(id);
        }

        public Task<bool> UpdateAsync(long id, Review entityModel)
        {
            return _reviewRepo.UpdateAsync(id, entityModel);
        }
    }
}
