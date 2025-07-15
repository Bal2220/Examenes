namespace Example_EF_1.EasyIrriot.Domain.Models.Commands;

public record CreateThingsCommand(
    string Model,
    decimal MaximumTemperatureThreshold,
    decimal MinimumHumidityThreshold);