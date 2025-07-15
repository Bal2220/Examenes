namespace Example_EF_1.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}