using Example_EF_1.Shared.Domain.Repositories;
using Example_EF_1.Shared.Infraestructure.Persistence.Configuration;

namespace Example_EF_1.Shared.Infraestructure.Persistence.Repositories;

public class UnitOfWork(EasyIrriotContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}