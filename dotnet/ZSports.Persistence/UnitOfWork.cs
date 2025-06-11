using ZSports.Contracts;

namespace ZSports.Persistence;

public class UnitOfWork(ZSportsDbContext dbContext) : IUnitOfWork
{
    private bool _disposed;
    private readonly Dictionary<(Type, Type), object> _repositories = new();

    public IGenericRepository<TItem, TKey> GetRepository<TItem, TKey>() where TItem : class
    {
        var key = (typeof(TItem), typeof(TKey));
        if (!_repositories.TryGetValue(key, out var repository))
        {
            repository = new GenericRepository<TItem, TKey>(dbContext);
            _repositories[key] = repository;
        }
        return (IGenericRepository<TItem, TKey>)repository;
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            _disposed = true;
        }
    }
}
