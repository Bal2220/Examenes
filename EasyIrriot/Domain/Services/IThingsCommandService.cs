using Example_EF_1.EasyIrriot.Domain.Models.Commands;
using Example_EF_1.EasyIrriot.Domain.Models.Entities;
using Example_EF_1.Shared.Domain.Repositories;

namespace Example_EF_1.EasyIrriot.Domain.Services;

public interface IThingsCommandService
{
    Task<Thing> Handle(CreateThingsCommand command);
}