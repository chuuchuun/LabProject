using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.ReviewDtos;
using LabProject.Domain.Entities;

namespace LabProject.Application.Interfaces
{
    public interface IReviewService : IBaseService<Review, ReviewDto, ReviewCreateDto, ReviewUpdateDto>
    {
        public Task<double> GetAverageRatingForProviderAsync(long providerId);
    }
}
