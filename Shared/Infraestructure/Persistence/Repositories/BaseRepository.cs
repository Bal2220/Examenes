using Example_EF_1.Shared.Domain.Repositories;
using Example_EF_1.Shared.Infraestructure.Persistence.Configuration;

namespace Example_EF_1.Shared.Infraestructure.Persistence.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly EasyIrriotContext Context;

    protected BaseRepository(EasyIrriotContext context)
    {
        Context = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }
    
    public async Task<TEntity?> FindByIdAsync(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }
    
    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }
}