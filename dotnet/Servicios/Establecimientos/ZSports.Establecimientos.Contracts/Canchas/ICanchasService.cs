using ZSports.Contracts;
using ZSports.Establecimientos.Contracts.Canchas.CrearCancha;
using ZSports.Establecimientos.Contracts.Canchas.ModificarCancha;

namespace ZSports.Establecimientos.Contracts.Canchas;

/// <summary>
/// Servicio para la gestión de canchas en establecimientos deportivos.
/// </summary>
public interface ICanchasService
{
    /// <summary>
    /// Agrega una nueva cancha al establecimiento.
    /// </summary>
    /// <param name="cancha">Datos de la cancha a agregar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>El identificador de la nueva cancha.</returns>
    Task<CanchaDto> AgregarAsync(CrearCanchaRequest cancha, CancellationToken cancellationToken);
    /// <summary>
    /// Obtiene una cancha por su identificador.
    /// </summary>
    /// <param name="id">Identificador de la cancha.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>La cancha encontrada o null si no existe.</returns>
    Task<CanchaDto> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Obtiene una lista paginada de canchas.
    /// </summary>
    /// <param name="paginationInfo">Información de paginación.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista paginada de canchas.</returns>
    Task<IEnumerable<CanchaDto>> ObtenerPaginadoAsync(GetItemsPaginated paginationInfo, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una lista de canchas por el identificador del establecimiento.
    /// </summary>
    /// <param name="establecimientoId">El identificador del establecimiento.</param>
    /// <param name="paginationInfo">La informacion de paginacion.</param>
    /// <param name="cancellationToken">El token de cancelacion.</param>
    /// <returns>Una lista paginada de canchas pertenecientes al establecimiento.</returns>
    Task<IEnumerable<CanchaDto>> ObtenerPorEstablecimientoAsync(Guid establecimientoId, GetItemsPaginated paginationInfo, CancellationToken cancellationToken);

    /// <summary>
    /// Modifica una cancha existente.
    /// </summary>
    /// <param name="cancha">Datos nuevos de la cancha.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>True si la modificación fue exitosa, false si no se encontró.</returns>
    Task<CanchaDto> ModificarAsync(ModificarCanchaRequest cancha, CancellationToken cancellationToken);
    /// <summary>
    /// Elimina una cancha por su identificador.
    /// </summary>
    /// <param name="id">Identificador de la cancha a eliminar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    Task EliminarAsync(Guid id, CancellationToken cancellationToken);
}
