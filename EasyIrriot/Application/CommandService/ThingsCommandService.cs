using Example_EF_1.EasyIrriot.Domain;
using Example_EF_1.EasyIrriot.Domain.Models.Commands;
using Example_EF_1.EasyIrriot.Domain.Models.Entities;
using Example_EF_1.EasyIrriot.Domain.Services;
using Example_EF_1.Shared.Domain.Repositories;
using FluentValidation;

namespace Example_EF_1.EasyIrriot.Application.CommandService
{
    public class ThingsCommandService(
        IThingsRepository thingRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateThingsCommand> validator) : IThingsCommandService
    {
        private readonly IThingsRepository _thingRepository = 
            thingRepository ?? throw new ArgumentNullException(nameof(thingRepository));
        
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        
        private readonly IValidator<CreateThingsCommand> _validator =
            validator ?? throw new ArgumentNullException(nameof(validator));

        public async Task<Thing> Handle(CreateThingsCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);
            
            // Errors with Validator
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(string.Join(", ", errors));
            }
            
            // Create Thing
            var thing = new Thing(
                command.Model,
                command.MaximumTemperatureThreshold,
                command.MinimumHumidityThreshold
            );
            await _thingRepository.AddAsync(thing);
            await _unitOfWork.CompleteAsync();
            
            return thing;
        }
    }
}