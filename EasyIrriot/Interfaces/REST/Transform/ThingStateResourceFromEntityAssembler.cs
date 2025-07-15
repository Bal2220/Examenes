using Example_EF_1.EasyIrriot.Domain.Models.Entities;
using Example_EF_1.EasyIrriot.Interfaces.REST.Resources;

namespace Example_EF_1.EasyIrriot.Interfaces.REST.Transform;

public static class ThingStateResourceFromEntityAssembler
{
    public static ThingStateResource ToResource(ThingState state)
    {
        return new ThingStateResource(
            state.ThingSerialNumber,
            state.CurrentOperationMode,
            state.CurrentTemperature,
            state.CurrentHumidity,
            state.CollectedAt
        );
    }
}