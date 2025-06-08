using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LabProject.Application.Features.Locations.Commands;

namespace LabProject.Application.Validators
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(x => x.Dto.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(200);
            RuleFor(x => x.Dto.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(100);
            RuleFor(x => x.Dto.Phone)
                .MaximumLength(20)
                .Matches(@"^\+?[0-9\s\-()]+$").WithMessage("Phone must be a valid phone number format");
        }
    }
}
