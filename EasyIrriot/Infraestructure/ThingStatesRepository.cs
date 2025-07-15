using Example_EF_1.EasyIrriot.Domain;
using Example_EF_1.EasyIrriot.Domain.Models.Entities;
using Example_EF_1.Shared.Infraestructure.Persistence.Configuration;
using Example_EF_1.Shared.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Example_EF_1.EasyIrriot.Infraestructure;

public class ThingStatesRepository(EasyIrriotContext context) : BaseRepository<ThingState>(context), IThingStatesRepository
{
    public async Task<ThingState?> FindBySerialAndDateAsync(string serialNumber, DateTime collectedAt)
    {
        return await Context.Set<ThingState>()
            .FirstOrDefaultAsync(t => 
                t.ThingSerialNumber == serialNumber &&
                t.CollectedAt == collectedAt);
    }
}