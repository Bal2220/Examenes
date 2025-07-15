using Example_EF_1.EasyIrriot.Domain;
using Example_EF_1.EasyIrriot.Domain.Models.Entities;
using Example_EF_1.Shared.Infraestructure.Persistence.Configuration;
using Example_EF_1.Shared.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Example_EF_1.EasyIrriot.Infraestructure;

public class ThingsRepository(EasyIrriotContext context) : BaseRepository<Thing>(context), IThingsRepository
{
    public async Task<Thing?> FindBySerialNumberAsync(string serialNumber)
    {
        return await Context.Set<Thing>().FirstOrDefaultAsync(t => t.SerialNumber == serialNumber);
    }
    
    public async Task<Thing?> FindByIdAsync(int id)
    {
        return await Context.Set<Thing>()
            .Include(t => t.ThingStates)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

}