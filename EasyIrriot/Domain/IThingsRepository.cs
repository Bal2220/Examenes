using Example_EF_1.EasyIrriot.Domain.Models.Commands;
using Example_EF_1.EasyIrriot.Domain.Models.Entities;
using Example_EF_1.Shared.Domain.Repositories;

namespace Example_EF_1.EasyIrriot.Domain;

public interface IThingsRepository : IBaseRepository<Thing>
{
    Task<Thing?> FindBySerialNumberAsync(string serialNumber);
    Task<Thing?> FindByIdAsync(int id);
}