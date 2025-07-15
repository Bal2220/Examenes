namespace Example_EF_1.EasyIrriot.Interfaces.REST.Resources;

public record ThingStateResource(
    string ThingSerialNumber,
    int CurrentOperationMode,
    decimal CurrentTemperature,
    decimal CurrentHumidity,
    DateTime CollectedAt
);
