using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LabProject.Application.Features.Users.Commands;

namespace LabProject.Application.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Dto.Name)
                .MaximumLength(100);
            RuleFor(x => x.Dto.Phone)
                .MaximumLength(20)
                .Matches(@"^\+?[0-9\s\-()]+$").WithMessage("Phone must be a valid phone number format");
            RuleFor(x => x.Dto.Email)
                .MaximumLength(255)
                .EmailAddress().WithMessage("Email must be a valid email format");
            RuleFor(x => x.Dto.Password)
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
                .When(x => !string.IsNullOrEmpty(x.Dto.Password));
        }
    }
}
