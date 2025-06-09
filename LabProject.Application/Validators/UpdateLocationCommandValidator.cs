using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LabProject.Application.Features.Locations.Commands;

namespace LabProject.Application.Validators
{
    public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationCommandValidator() {
            RuleFor(x => x.Dto.Name)
            .MaximumLength(100);
            RuleFor(x => x.Dto.Address)
                .MaximumLength(255);
            RuleFor(x => x.Dto.Phone)
                .Matches(@"^\d{3}-\d{3}-\d{4}$").WithMessage("Phone must be in the format XXX-XXX-XXXX")
                .When(x => !string.IsNullOrEmpty(x.Dto.Phone));
            RuleFor(x => x.Dto.City)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.Dto.City));
        }
    }
}
