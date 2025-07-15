using Example_EF_1.EasyIrriot.Domain.Models.Commands;
using Example_EF_1.EasyIrriot.Domain.Models.Entities;

namespace Example_EF_1.EasyIrriot.Domain.Services;

public interface IThingStatesCommandService
{
    Task<ThingState> Handle(CreateThingStatesCommand command);
}