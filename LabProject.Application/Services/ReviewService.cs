using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.ReviewDtos;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Hubs;
using LabProject.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace LabProject.Application.Services
{
    public class ReviewService(IReviewRepository reviewRepo, IMapper mapper, IHubContext<ReviewHub> hubContext) : IReviewService
    {
        private readonly IReviewRepository _reviewRepo = reviewRepo;
        private readonly IMapper _mapper = mapper;
        private readonly IHubContext<ReviewHub> _hubContext = hubContext;

        public async Task<long> AddAsync(ReviewCreateDto reviewCreateDto)
        {
            var reviewEntity = _mapper.Map<Review>(reviewCreateDto);
            reviewEntity.CreatedAt = DateTime.UtcNow;
            reviewEntity.UpdatedAt = DateTime.UtcNow;

            var id = await _reviewRepo.AddAsync(reviewEntity);
            var message = $"New review added by user {reviewCreateDto.ClientId} for appointment {reviewCreateDto.AppointmentId}";
            await _hubContext.Clients.All.SendAsync("ReceiveReviewNotification", message);
            return id;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _reviewRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<ReviewDto>> GetAllAsync()
        {
            var reviews = await _reviewRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<double> GetAverageRatingForProviderAsync(long providerId)
        {
            return await _reviewRepo.GetAverageRatingForProviderAsync(providerId);
        }

        public async Task<ReviewDto?> GetByIdAsync(long id)
        {
            var review = await _reviewRepo.GetByIdAsync(id);
            return review is null ? null : _mapper.Map<ReviewDto?>(review);
        }

        public async Task<bool> UpdateAsync(long id, ReviewUpdateDto reviewUpdateDto)
        {
            var existingReview = await _reviewRepo.GetByIdAsync(id);
            if (existingReview is null)
                return false;

            _mapper.Map(reviewUpdateDto, existingReview);
            existingReview.UpdatedAt = DateTime.UtcNow;

            return await _reviewRepo.UpdateAsync(id, existingReview);
        }
    }
}
