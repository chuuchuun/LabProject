using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.DiscountDtos;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class DiscountService(IDiscountRepository discountRepo, IMapper mapper) : IDiscountService
    {
        private readonly IDiscountRepository _discountRepo = discountRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<long> AddAsync(DiscountCreateDto discountCreateDto)
        {
            var discountEntity = _mapper.Map<Discount>(discountCreateDto);
            discountEntity.CreatedAt = DateTime.UtcNow;
            discountEntity.UpdatedAt = DateTime.UtcNow;

            return await _discountRepo.AddAsync(discountEntity);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _discountRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<DiscountDto>> GetAllAsync()
        {
            var discounts = await _discountRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<DiscountDto>>(discounts);
        }

        public async Task<DiscountDto?> GetByIdAsync(long id)
        {
            var discount = await _discountRepo.GetByIdAsync(id);
            return discount is null ? null : _mapper.Map<DiscountDto>(discount);
        }

        public async Task<IEnumerable<DiscountDto>> GetValidDiscountsForClientAsync(long clientId)
        {
            var validDiscounts = await _discountRepo.GetValidDiscountsForClientAsync(clientId);
            return _mapper.Map<IEnumerable<DiscountDto>>(validDiscounts);
        }

        public async Task<bool> UpdateAsync(long id, DiscountUpdateDto discountUpdateDto)
        {
            var exisitngDiscount = await _discountRepo.GetByIdAsync(id);
            if (exisitngDiscount is null)
                return false;

            _mapper.Map(discountUpdateDto, exisitngDiscount);
            exisitngDiscount.UpdatedAt = DateTime.UtcNow;

            return await _discountRepo.UpdateAsync(id, exisitngDiscount);
        }
    }
}
