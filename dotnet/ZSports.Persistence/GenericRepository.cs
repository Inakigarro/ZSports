using Microsoft.EntityFrameworkCore;
using ZSports.Contracts;

namespace ZSports.Persistence;

public class GenericRepository<TItem, TKey>(ZSportsDbContext dbContext) : IGenericRepository<TItem, TKey>
    where TItem : class
{
    /// <inheritdoc/>
    public async Task AddAsync(TItem item, CancellationToken cancellationToken)
    {
        await dbContext.Set<TItem>().AddAsync(item, cancellationToken);
    }

    /// <inheritdoc/>
    public void Update(TItem item)
    {
        dbContext.Set<TItem>().Update(item);
    }

    /// <inheritdoc/>
    public async Task<TItem> GetByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<TItem>().FindAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Item with id {id} not found.");
    }

    public IQueryable<TItem> GetAsQueryable()
    {
        return dbContext.Set<TItem>().AsQueryable();
    }

    /// <inheritdoc/>
    public void Delete(TItem item)
    {
        dbContext.Set<TItem>().Remove(item);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TItem>> GetAllAsync(GetItemsPaginated paginationInfo, CancellationToken cancellationToken)
    {
        return await dbContext.Set<TItem>()
            .Skip(paginationInfo.PageNumber * paginationInfo.PageSize)
            .Take(paginationInfo.PageSize)
            .ToListAsync(cancellationToken);
    }
}