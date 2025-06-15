namespace ZSports.Contracts;

public interface IGenericRepository<TItem, TKey> where TItem : class
{
    /// <summary>
    /// Agrega un Item a la base de datos.
    /// </summary>
    /// <param name="item">El Item a agregar.</param>
    /// <param name="cancellationToken">El token de cancelacion de la request.</param>
    Task AddAsync(TItem item, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un Item por su Id.
    /// </summary>
    /// <param name="id">El Id del Item a buscar.</param>
    /// <param name="cancellationToken">El token de cancelacion de la request.</param>
    /// <returns>El Item correspondiente al Id proveido.</returns>
    Task<TItem> GetByIdAsync(TKey id, CancellationToken cancellationToken);

    /// <summary>
    /// Actualiza un Item en la base de datos.
    /// </summary>
    /// <param name="item">El item actualizado.</param>
    void Update(TItem item);

    /// <summary>
    /// Obtiene una lista paginada de Items.
    /// </summary>
    /// <param name="cancellationToken">El token de cancelacion de la request.</param>
    /// <returns>Una lista paginada de Items.</returns>
    Task<IEnumerable<TItem>> GetAllAsync(GetItemsPaginated paginationInfo, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un IQueryable de Items.
    /// </summary>
    IQueryable<TItem> GetAsQueryable();

    /// <summary>
    /// Elimina un Item de la base de datos por su Id.
    /// </summary>
    /// <param name="item">El Item a eliminar.</param>
    void Delete(TItem item);
}
