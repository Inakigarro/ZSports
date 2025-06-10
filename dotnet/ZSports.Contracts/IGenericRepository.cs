namespace ZSports.Contracts;

public interface IGenericRepository<TItem, TKey>
{
    Task<TItem?> GetByIdAsync(TKey id);
    Task<IEnumerable<TItem>> GetAllAsync();
    Task AddAsync(TItem item);
    Task UpdateAsync(TItem item);
    Task DeleteAsync(TKey id);
}
