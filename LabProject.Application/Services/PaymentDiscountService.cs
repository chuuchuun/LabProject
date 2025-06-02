using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.PaymentDiscountDtos;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class PaymentDiscountService(IRepository<PaymentDiscount> paymentDiscountRepo, IMapper mapper) : IReadableService<PaymentDiscountDto>, IDeletableService
    {
        private readonly IRepository<PaymentDiscount> _paymentDiscountRepo = paymentDiscountRepo;
        private readonly IMapper _mapper = mapper;
        
        public async Task<bool> DeleteAsync(long id)
        {
            return await _paymentDiscountRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<PaymentDiscountDto>> GetAllAsync()
        {
            var paymentDiscounts = await _paymentDiscountRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentDiscountDto>>(paymentDiscounts);
        }

        public async Task<PaymentDiscountDto?> GetByIdAsync(long id)
        {
            var paymentDiscounts = await _paymentDiscountRepo.GetByIdAsync(id);
            return paymentDiscounts is null ? null : _mapper.Map<PaymentDiscountDto>(paymentDiscounts);
        }
    }
}
