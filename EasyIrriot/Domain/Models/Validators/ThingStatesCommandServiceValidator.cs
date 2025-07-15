using Example_EF_1.EasyIrriot.Domain.Models.Commands;
using FluentValidation;

namespace Example_EF_1.EasyIrriot.Domain.Models.Validators;

public class ThingStatesCommandServiceValidator : AbstractValidator<CreateThingStatesCommand>
{
    public ThingStatesCommandServiceValidator()
    {
        RuleFor(v => v.ThingSerialNumber)
            .NotNull()
            .NotEmpty()
            .WithMessage("Thing Serial Number cannot be null or empty");
        
        RuleFor(v => v.CurrentOperationMode)
            .NotNull()
            .NotEmpty()
            .WithMessage("Operation mode cannot be null or empty");
        
        RuleFor(v => v.CurrentOperationMode)
            .InclusiveBetween(Constants.MinOperationMode, Constants.MaxOperationMode)
            .WithMessage($"Current operation mode must be between {Constants.MinOperationMode} and {Constants.MaxOperationMode}.");

        RuleFor(v => v.CurrentTemperature)
            .NotNull()
            .NotEmpty()
            .WithMessage("Current temperature cannot be null or empty");
        
        RuleFor(v => v.CurrentHumidity)
            .NotNull()
            .NotEmpty()
            .WithMessage("Current humidity cannot be null or empty");
        
        RuleFor(v => v.CollectedAt)
            .NotNull()
            .NotEmpty()
            .WithMessage("CollectedAt cannot be null or empty");
    }
    private static class Constants
    {
        public const int MinOperationMode = 0;
        public const int MaxOperationMode = 2;
    }
}