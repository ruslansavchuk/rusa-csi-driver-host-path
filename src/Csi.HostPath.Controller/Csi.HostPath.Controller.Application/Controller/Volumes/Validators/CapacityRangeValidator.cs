using Csi.HostPath.Controller.Application.Common.Dto;
using FluentValidation;

namespace Csi.HostPath.Controller.Application.Controller.Volumes.Validators;

public class CapacityRangeValidator : AbstractValidator<CapacityRangeDto>
{
    public CapacityRangeValidator()
    {
        RuleFor(i => i.Required).GreaterThanOrEqualTo(0);
        RuleFor(i => i.Limit).GreaterThanOrEqualTo(0);
    }
}