namespace ZSports.Contracts;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
    IGenericRepository<TItem, TKey> GetRepository<TItem, TKey>() where TItem : class;
}
