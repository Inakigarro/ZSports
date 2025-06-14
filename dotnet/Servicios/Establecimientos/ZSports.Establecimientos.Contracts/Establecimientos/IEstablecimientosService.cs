using ZSports.Contracts;
using ZSports.Establecimientos.Contracts.Establecimientos.CrearEstablecimiento;
using ZSports.Establecimientos.Contracts.Establecimientos.ModificarEstablecimiento;

namespace ZSports.Establecimientos.Contracts.Establecimientos;

/// <summary>
/// Servicio para la gestión de establecimientos.
/// </summary>
public interface IEstablecimientosService
{
    /// <summary>
    /// Agrega un nuevo establecimiento.
    /// </summary>
    /// <param name="establecimiento">Entidad de establecimiento a agregar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>El identificador del nuevo establecimiento.</returns>
    Task<EstablecimientoDto> AgregarAsync(CrearEstablecimientoRequest establecimiento, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un establecimiento por su identificador.
    /// </summary>
    /// <param name="id">Identificador del establecimiento.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>El establecimiento encontrado o null si no existe.</returns>
    Task<EstablecimientoDto> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una lista paginada de establecimientos.
    /// </summary>
    /// <param name="paginationInfo">Información de paginación.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista paginada de establecimientos.</returns>
    Task<IEnumerable<EstablecimientoDto>> ObtenerPaginadoAsync(GetItemsPaginated paginationInfo, CancellationToken cancellationToken);

    /// <summary>
    /// Modifica un establecimiento existente.
    /// </summary>
    /// <param name="id">Identificador del establecimiento a modificar.</param>
    /// <param name="establecimiento">Datos nuevos del establecimiento.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>True si la modificación fue exitosa, false si no se encontró.</returns>
    Task<EstablecimientoDto> ModificarAsync(ModificarEstablecimientoRequest establecimiento, CancellationToken cancellationToken);

    /// <summary>
    /// Elimina un establecimiento por su identificador.
    /// </summary>
    /// <param name="id">Identificador del establecimiento a eliminar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    Task EliminarAsync(Guid id, CancellationToken cancellationToken);
}
