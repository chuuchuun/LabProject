using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LabProject.Application.Features.Services.Commands;

namespace LabProject.Application.Validators
{
    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceCommandValidator()
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty().WithMessage("Service name is required")
                .MaximumLength(100);
            RuleFor(x => x.Dto.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");
            RuleFor(x => x.Dto.DurationMinutes)
                .GreaterThan(0).WithMessage("Duration must be greater than 0")
                .LessThanOrEqualTo(180).WithMessage("Duration cannot exceed 180 minutes (3 hours)");
            RuleFor(x => x.Dto.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
