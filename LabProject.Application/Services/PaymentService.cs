using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.PaymentDtos;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class PaymentService(IPaymentRepository paymentRepo, IMapper mapper) : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo = paymentRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<long> AddAsync(PaymentCreateDto paymentCreateDto)
        {
            var paymentEntity = _mapper.Map<Payment>(paymentCreateDto);
            paymentEntity.CreatedAt = DateTime.UtcNow;
            paymentEntity.UpdatedAt = DateTime.UtcNow;

            return await _paymentRepo.AddAsync(paymentEntity);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _paymentRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<PaymentDto>> GetAllAsync()
        {
            var payments =  await _paymentRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto?> GetByIdAsync(long id)
        {
            var payment = await _paymentRepo.GetByIdAsync(id);
            return payment is null ? null : _mapper.Map<PaymentDto>(payment);
        }

        public async Task<bool> UpdateAsync(long id, Payment entityModel)
        {
            return await _paymentRepo.UpdateAsync(id, entityModel);
        }
        public async Task<decimal> GetTotalRevenueByProviderAsync(long providerId, DateTime startDate, DateTime endDate)
        {
            return await _paymentRepo.GetTotalRevenueByProviderAsync(providerId, startDate, endDate);
        }
    }
}
