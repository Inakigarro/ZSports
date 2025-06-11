namespace ZSports.Contracts;

public record GetItemsPaginated
{
    /// <summary>
    /// Numero de la pagina a buscar.
    /// </summary>
    public int PageNumber { get; init; } = 1;

    /// <summary>
    /// Cantidad de items por pagina.
    /// </summary>
    public int PageSize { get; init; } = 10;
}
