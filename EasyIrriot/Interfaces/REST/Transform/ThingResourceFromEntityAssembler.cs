using Example_EF_1.EasyIrriot.Domain.Models.Entities;
using Example_EF_1.EasyIrriot.Interfaces.REST.Resources;

namespace Example_EF_1.EasyIrriot.Interfaces.REST.Transform;

public static class ThingResourceFromEntityAssembler
{
    public static ThingResource ToResource(Thing thing)
    {
        return new ThingResource(
            thing.Id,
            thing.SerialNumber,
            thing.Model,
            thing.OperationMode,
            thing.MaximumTemperatureThreshold,
            thing.MinimumHumidityThreshold,
            thing.ThingStates?.Select(state => new ThingStateResource(
                state.Id.ToString(),
                state.CurrentOperationMode,
                state.CurrentTemperature,
                state.CurrentHumidity,
                state.CollectedAt
            )).ToList() ?? new List<ThingStateResource>()
        );
    }

}