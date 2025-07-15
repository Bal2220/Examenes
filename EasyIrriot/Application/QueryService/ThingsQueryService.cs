using Example_EF_1.EasyIrriot.Domain;
using Example_EF_1.EasyIrriot.Domain.Models.Entities;
using Example_EF_1.EasyIrriot.Domain.Models.Queries;
using Example_EF_1.EasyIrriot.Domain.Services;

namespace Example_EF_1.EasyIrriot.Application.QueryService
{
    public class ThingsQueryService(IThingsRepository thingRepository) : IThingsQueryService
    {
        private readonly IThingsRepository _thingRepository = thingRepository ?? throw new ArgumentNullException(nameof(thingRepository));
        
        public async Task<Thing?> Handle(GetThingsByIdQuery query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var thing = await _thingRepository.FindByIdAsync(query.ThingId);
            return thing;
        }
    }
}