using Example_EF_1.EasyIrriot.Domain.Models.Entities;
using Example_EF_1.Shared.Domain.Repositories;

namespace Example_EF_1.EasyIrriot.Domain;

public interface IThingStatesRepository : IBaseRepository<ThingState>
{
    Task<ThingState?> FindBySerialAndDateAsync(string serialNumber, DateTime collectedAt);
}