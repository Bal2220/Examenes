using Example_EF_1.EasyIrriot.Domain;
using Example_EF_1.EasyIrriot.Domain.Models.Commands;
using Example_EF_1.EasyIrriot.Domain.Models.Entities;
using Example_EF_1.EasyIrriot.Domain.Services;
using Example_EF_1.Shared.Domain.Repositories;
using FluentValidation;

namespace Example_EF_1.EasyIrriot.Application.CommandService
{
    public class ThingStatesCommandService(
        IThingStatesRepository thingStatesRepository,
        IThingsRepository thingsRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateThingStatesCommand> validator) : IThingStatesCommandService
    {
        private readonly IThingStatesRepository _thingStatesRepository = 
            thingStatesRepository ?? throw new ArgumentNullException(nameof(thingStatesRepository));
        private readonly IThingsRepository _thingsRepository = 
            thingsRepository ?? throw new ArgumentNullException(nameof(thingsRepository));
        
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        
        private readonly IValidator<CreateThingStatesCommand> _validator =
            validator ?? throw new ArgumentNullException(nameof(validator));

        public async Task<ThingState> Handle(CreateThingStatesCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);
            
            // Errors with Validator
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(string.Join(", ", errors));
            }
            
            // Error: Not exist thingSerialNumber
            var thing = await _thingsRepository.FindBySerialNumberAsync(command.ThingSerialNumber.ToString());
            if (thing is null)
                throw new ArgumentException("The provided serial number does not match any registered Thing.");
            
            // Error: Same combination of thingSerialNumber and collectedAt
            var existingState = await _thingStatesRepository.FindBySerialAndDateAsync(
                command.ThingSerialNumber.ToString(), command.CollectedAt);
            if (existingState is not null)
                throw new ArgumentException("There is already a state with the same serial number and timestamp.");

            // Error: Date is future
            if (command.CollectedAt > DateTime.UtcNow)
                throw new ArgumentException("The collectedAt value cannot be in the future.");
            
            // Create ThingState
            var thingState = new ThingState
            {
                ThingSerialNumber = command.ThingSerialNumber.ToString(),
                ThingId = thing.Id,
                CurrentOperationMode = command.CurrentOperationMode,
                CurrentTemperature = command.CurrentTemperature,
                CurrentHumidity = command.CurrentHumidity,
                CollectedAt = command.CollectedAt,
                CreatedAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
            };
            
            thing.UpdateAt = DateTime.UtcNow;
            _thingsRepository.Update(thing);
            
            await _thingStatesRepository.AddAsync(thingState);
            await _unitOfWork.CompleteAsync();
            
            return thingState;
        }
    }
}