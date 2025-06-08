using FluentValidation;
using LabProject.Application.Dtos.AppontmentDtos;
using LabProject.Application.Dtos.LocationDtos;
using LabProject.Application.Dtos.PaymentDiscountDtos;
using LabProject.Application.Dtos.RoleDtos;
using LabProject.Application.Interfaces;
using LabProject.Application.Services;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using LabProject.Infrastructure.Interfaces;
using LabProject.Infrastructure.Repositories;
using LabProject.Infrastructure;

namespace LabProject.Presentation
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddApplicationServiceRegistration(this IServiceCollection services)
        {

            services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IRepository<Appointment>, AppointmentRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IRepository<Location>, LocationRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IRepository<PaymentDiscount>, PaymentDiscountRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddScoped<IBaseService<Appointment, AppointmentDto, AppointmentCreateDto, AppointmentUpdateDto>, AppointmentService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IBaseService<Location, LocationDto, LocationCreateDto, LocationUpdateDto>, LocationService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IReadableService<PaymentDiscountDto>, PaymentDiscountService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IBaseService<Role, RoleDto, RoleCreateDto, RoleUpdateDto>, RoleService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IUserService, UserService>();

            
            return services;
        }
    }
}
