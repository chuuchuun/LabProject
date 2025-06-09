using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LabProject.Application.Features.Services.Commands;

namespace LabProject.Application.Validators
{
    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceCommandValidator() {
            RuleFor(x => x.Dto.Name)
                .MaximumLength(100).WithMessage("Service name must not exceed 100 characters.");
            RuleFor(x => x.Dto.Description)
                .MaximumLength(1000).WithMessage("Service description must not exceed 1000 characters.");
            RuleFor(x => x.Dto.DurationMinutes)
                .GreaterThan(0).WithMessage("Service duration must be greater than 0 minutes.")
                .LessThanOrEqualTo(180).WithMessage("Service duration must not exceed 180 minutes (3 hours).");
            RuleFor(x => x.Dto.Price)
                .GreaterThan(0).WithMessage("Service price must be greater than 0.");
        }
    }
}
