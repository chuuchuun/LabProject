using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Entities;

namespace LabProject.Domain.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        public Task<double> GetAverageRatingForProviderAsync(long providerId);
    }
}
