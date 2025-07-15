namespace Example_EF_1.EasyIrriot.Domain.Models.Commands;

public record CreateThingStatesCommand( 
    Guid ThingSerialNumber,
    int CurrentOperationMode,
    decimal CurrentTemperature,
    decimal CurrentHumidity,
    DateTime CollectedAt);