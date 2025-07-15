using Example_EF_1.EasyIrriot.Domain.Models.Entities;
using Example_EF_1.EasyIrriot.Domain.Models.Queries;

namespace Example_EF_1.EasyIrriot.Domain.Services;

public interface IThingsQueryService
{
    Task<Thing> Handle(GetThingsByIdQuery query);
}