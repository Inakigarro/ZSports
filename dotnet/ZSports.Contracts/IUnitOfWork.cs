namespace ZSports.Contracts;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync();
    IGenericRepository<TItem, TKey> GetRepository<TItem, TKey>();
}
