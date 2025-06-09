using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Features.Users.Commands;

namespace LabProject.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        static readonly string[] roles = ["Admin", "Client", "Provider"];
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);

            RuleFor(x => x.Dto.Username)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(50);

            RuleFor(x => x.Dto.Email)
                .NotEmpty().WithMessage("Email is required")
                .MaximumLength(100)
                .EmailAddress();

            RuleFor(x => x.Dto.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6);
            RuleFor(x => x.Dto.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .Matches(@"^\d{3}-\d{3}-\d{4}$").WithMessage("Phone must be in the format XXX-XXX-XXXX");
            RuleFor(x => x.Dto.RoleName)
                .NotEmpty().WithMessage("Role is required")
                .Must(role =>
                {
                    return roles.Contains(role);
                })
                .WithMessage("Role must be either Admin, Client, or Provider");
        }
    }
}
