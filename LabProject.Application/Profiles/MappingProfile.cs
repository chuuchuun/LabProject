// MappingProfile.cs
using AutoMapper;
using LabProject.Application.Dtos.AppontmentDtos;
using LabProject.Application.Dtos.DiscountDtos;
using LabProject.Application.Dtos.LocationDtos;
using LabProject.Application.Dtos.PaymentDiscountDtos;
using LabProject.Application.Dtos.PaymentDtos;
using LabProject.Application.Dtos.ReviewDtos;
using LabProject.Application.Dtos.RoleDtos;
using LabProject.Application.Dtos.ServiceDtos;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Dtos;
using LabProject.Domain.Entities;
using LabProject.Domain.Enums;

namespace LabProject.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseEntity, BaseDto>().ReverseMap();

            CreateAppointmentMappings();
            CreateDiscountMappings();
            CreateLocationMappings();
            CreatePaymentMappings();
            CreatePaymentDiscountMappings();
            CreateReviewMappings();
            CreateRoleMappings();
            CreateServiceMappings();
            CreateUserMappings();
        }

        private void CreateAppointmentMappings()
        {
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<AppointmentStatus>(src.Status)));

            CreateMap<Appointment, AppointmentCreateDto>().ReverseMap();
            CreateMap<Appointment, AppointmentUpdateDto>().ReverseMap();
            CreateMap<Appointment, AppointmentClientViewDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();

            CreateMap<Appointment, AppointmentBasicDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();
        }

        private void CreateDiscountMappings()
        {
            CreateMap<Discount, DiscountDto>().ReverseMap();
            CreateMap<Discount, DiscountCreateDto>().ReverseMap();
            CreateMap<Discount, DiscountUpdateDto>().ReverseMap();
            CreateMap<Discount, DiscountBasicDto>().ReverseMap();
        }

        private void CreateLocationMappings()
        {
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Location, LocationCreateDto>().ReverseMap();
            CreateMap<Location, LocationUpdateDto>().ReverseMap();
            CreateMap<Location, LocationBasicDto>().ReverseMap();
        }

        private void CreatePaymentMappings()
        {
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Payment, PaymentCreateDto>()
                .ReverseMap()
                .ForMember(dest => dest.PaidAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<Payment, PaymentBasicDto>().ReverseMap();
        }

        private void CreatePaymentDiscountMappings()
        {
            CreateMap<PaymentDiscount, PaymentDiscountDto>().ReverseMap();
        }

        private void CreateReviewMappings()
        {
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, ReviewCreateDto>().ReverseMap();
            CreateMap<Review, ReviewUpdateDto>().ReverseMap();
        }

        private void CreateRoleMappings()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, RoleCreateDto>().ReverseMap();
            CreateMap<Role, RoleUpdateDto>().ReverseMap();
        }

        private void CreateServiceMappings()
        {
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Service, ServiceCreateDto>().ReverseMap();
            CreateMap<Service, ServiceUpdateDto>().ReverseMap();
            CreateMap<Service, ServiceBasicDto>().ReverseMap();
        }

        private void CreateUserMappings()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role!.Name))
                .ReverseMap()
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<User, UserBasicDto>().ReverseMap();

            CreateMap<User, UserCreateDto>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<User, UserUpdateDto>()
                .ReverseMap()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<User, UserProviderDto>()
                .ReverseMap();
        }
    }
}
