using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZSports.Contracts;
using ZSports.Establecimientos.Contracts.Canchas;
using ZSports.Establecimientos.Contracts.Canchas.CrearCancha;
using ZSports.Establecimientos.Contracts.Canchas.ModificarCancha;
using ZSports.Establecimientos.Domain;
using ZSports.Establecimientos.Domain.Enums;

namespace ZSports.Establecimientos.Persistence;

public class CanchasService(ILogger<CanchasService> logger, IUnitOfWork unitOfWork) : ICanchasService
{
    public async Task<CanchaDto> AgregarAsync(CrearCanchaRequest cancha, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Iniciando el proceso de creación de una nueva cancha.");
            var nuevaCancha = new Cancha();
            nuevaCancha.SetNumero(cancha.Numero);
            nuevaCancha.SetTipoSuelo(cancha.TipoSuelo);
            nuevaCancha.SetEstablecimiento(cancha.EstablecimientoId);

            await unitOfWork.GetRepository<Cancha, Guid>().AddAsync(nuevaCancha, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Cancha creada con éxito. ID: {CanchaId}", nuevaCancha.Id);
            return new()
            {
                Id = nuevaCancha.Id,
                Numero = nuevaCancha.Numero,
                TipoSuelo = nuevaCancha.TipoSuelo,
                TipoSueloParseado = nuevaCancha.TipoSuelo.AsString(),
                EstablecimientoId = nuevaCancha.EstablecimientoId
            };
        }
        catch (Exception ex)
        {
            logger.LogError("Error al crear la cancha. Detalles: {ExceptionMessage}", ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<CanchaDto>> ObtenerPorEstablecimientoAsync(
        Guid establecimientoId,
        GetItemsPaginated paginationInfo,
        CancellationToken cancellationToken)
    {
        if (establecimientoId == Guid.Empty)
        {
            logger.LogError("El ID del establecimiento no puede ser un GUID vacío.");
            throw new ArgumentException("El ID del establecimiento no puede ser un GUID vacío.", nameof(establecimientoId));
        }

        try
        {
            logger.LogInformation("Obteniendo canchas para el establecimiento con ID: {EstablecimientoId}", establecimientoId);
            var canchas = await unitOfWork.GetRepository<Cancha, Guid>()
                .GetAsQueryable()
                .Where(c => c.EstablecimientoId == establecimientoId)
                .OrderBy(c => c.Numero)
                .Skip((paginationInfo.PageNumber - 1) * paginationInfo.PageSize)
                .Take(paginationInfo.PageSize)
                .ToListAsync(cancellationToken);

            logger.LogInformation("Canchas obtenidas exitosamente. Total: {TotalCanchas}", canchas.Count());
            return canchas.Select(c => new CanchaDto
            {
                Id = c.Id,
                Numero = c.Numero,
                TipoSuelo = c.TipoSuelo,
                TipoSueloParseado = c.TipoSuelo.AsString(),
                EstablecimientoId = c.EstablecimientoId
            });
        }
        catch (Exception ex)
        {
            logger.LogError("Error al obtener las canchas del establecimiento. Detalles: {ExceptionMessage}", ex.Message);
            throw;
        }
    }

    public Task EliminarAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<CanchaDto> ModificarAsync(ModificarCanchaRequest cancha, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Iniciando el proceso de edicion de la cancha N°: {numero}", cancha.Numero);
            var canchaExistente = await unitOfWork.GetRepository<Cancha, Guid>().GetByIdAsync(cancha.Id, cancellationToken);
            if (canchaExistente is null)
            {
                logger.LogError("No se encontro una cancha con Id: {id}", cancha.Id);
                throw new KeyNotFoundException($"No se encontró la cancha con Id: {cancha.Id}");
            }

            canchaExistente.SetNumero(cancha.Numero);
            canchaExistente.SetTipoSuelo(cancha.TipoSuelo);
            canchaExistente.SetEstablecimiento(cancha.EstablecimientoId);

            unitOfWork.GetRepository<Cancha, Guid>().Update(canchaExistente);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new()
            {
                Id = canchaExistente.Id,
                Numero = canchaExistente.Numero,
                TipoSuelo = canchaExistente.TipoSuelo,
                TipoSueloParseado = canchaExistente.TipoSuelo.AsString(),
                EstablecimientoId = canchaExistente.EstablecimientoId
            };
        }
        catch (Exception)
        {
            logger.LogError("Error al modificar la cancha con Id: {canchaId}", cancha.Id);
            throw;
        }
    }

    public Task<IEnumerable<CanchaDto>> ObtenerPaginadoAsync(GetItemsPaginated paginationInfo, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<CanchaDto> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Obteniendo cancha con Id: {canchaId}", id);
            var cancha = await unitOfWork.GetRepository<Cancha, Guid>()
                .GetByIdAsync(id, cancellationToken);

            if (cancha == null)
            {
                logger.LogError("No se encontró la cancha con Id: {canchaId}", id);
                throw new KeyNotFoundException($"No se encontró la cancha con Id: {id}");
            }

            logger.LogInformation("Cancha encontrada: {canchaId}", cancha.Id);
            return new CanchaDto
            {
                Id = cancha.Id,
                Numero = cancha.Numero,
                TipoSuelo = cancha.TipoSuelo,
                TipoSueloParseado = cancha.TipoSuelo.AsString(),
                EstablecimientoId = cancha.EstablecimientoId
            };
        }
        catch (Exception)
        {
            logger.LogError("Error al obtener la cancha con Id: {canchaId}", id);
            throw;
        }
    }
}
