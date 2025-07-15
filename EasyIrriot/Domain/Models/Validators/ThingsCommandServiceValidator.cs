using Example_EF_1.EasyIrriot.Domain.Models.Commands;
using FluentValidation;

namespace Example_EF_1.EasyIrriot.Domain.Models.Validators;

public class ThingsCommandServiceValidator : AbstractValidator<CreateThingsCommand>
{
    public ThingsCommandServiceValidator()
    {
        RuleFor(v => v.Model)
            .NotEmpty()
            .WithMessage("Model cannot be empty.");
        
        RuleFor(v => v.MaximumTemperatureThreshold)
            .NotNull()
            .NotEmpty()
            .WithMessage("Maximum temperature threshold cannot be null or empty.");
        
        RuleFor(v => v.MaximumTemperatureThreshold)
            .InclusiveBetween(Constants.MinTemp, Constants.MaxTemp)
            .WithMessage($"Maximum temperature threshold should be between {Constants.MinTemp} and {Constants.MaxTemp}.");
        
        RuleFor(v => v.MinimumHumidityThreshold)
            .NotNull()
            .NotEmpty()
            .WithMessage("Minimum humidity threshold cannot be null or empty.");
        
        RuleFor(v => v.MinimumHumidityThreshold)
            .InclusiveBetween(Constants.MinHumidity, Constants.MaxHumidity)
            .WithMessage($"Minimum humidity threshold should be between {Constants.MinHumidity} and {Constants.MaxHumidity}.");
    }

    private static class Constants
    {
        public const decimal MinTemp = -40.00m;
        public const decimal MaxTemp = 85.00m;
        public const decimal MinHumidity = 0.00m;
        public const decimal MaxHumidity = 100.00m;
    }
}