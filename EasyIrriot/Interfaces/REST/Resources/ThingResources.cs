using Example_EF_1.EasyIrriot.Domain.Models.Entities;

namespace Example_EF_1.EasyIrriot.Interfaces.REST.Resources;

public record ThingResource(
    int Id,
    string SerialNumber,
    string Model,
    EOperationMode OperationMode,
    decimal MaximumTemperatureThreshold,
    decimal MinimumHumidityThreshold,
    List<ThingStateResource> ThingStates
);