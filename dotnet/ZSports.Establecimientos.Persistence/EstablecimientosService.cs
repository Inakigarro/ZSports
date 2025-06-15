using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZSports.Contracts;
using ZSports.Establecimientos.Contracts.Canchas;
using ZSports.Establecimientos.Contracts.Establecimientos;
using ZSports.Establecimientos.Contracts.Establecimientos.CrearEstablecimiento;
using ZSports.Establecimientos.Contracts.Establecimientos.ModificarEstablecimiento;
using ZSports.Establecimientos.Domain;
using ZSports.Establecimientos.Domain.Enums;

namespace ZSports.Establecimientos.Persistence;

public class EstablecimientosService(ILogger<EstablecimientosService> logger, IUnitOfWork unitOfWork) : IEstablecimientosService
{
    /// <inheritdoc/>
    public async Task<EstablecimientoDto> AgregarAsync(CrearEstablecimientoRequest establecimiento, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Iniciando el proceso de creación de un nuevo establecimiento: {Nombre}", establecimiento.Nombre);
            Establecimiento nuevoEstablecimiento = new();
            nuevoEstablecimiento.SetNombre(establecimiento.Nombre);
            nuevoEstablecimiento.SetDireccion(establecimiento.Direccion);
            nuevoEstablecimiento.SetTelefono(establecimiento.Telefono);
            nuevoEstablecimiento.SetEmail(establecimiento.Email);

            await unitOfWork.GetRepository<Establecimiento, Guid>().AddAsync(nuevoEstablecimiento, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Establecimiento creado con éxito. ID: {EstablecimientoId}", nuevoEstablecimiento.Id);
            return new EstablecimientoDto
            {
                Id = nuevoEstablecimiento.Id,
                Nombre = nuevoEstablecimiento.Nombre,
                Direccion = nuevoEstablecimiento.Direccion,
                Telefono = nuevoEstablecimiento.Telefono,
                Email = nuevoEstablecimiento.Email
            };
        }
        catch (Exception ex)
        {
            logger.LogError("Error al crear el establecimiento {Nombre}. Detalles: {ExceptionMessage}", establecimiento.Nombre, ex.Message);
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<EstablecimientoDto> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Obteniendo establecimiento con ID: {EstablecimientoId}", id);
            var establecimiento = await unitOfWork
                .GetRepository<Establecimiento, Guid>()
                .GetByIdAsync(id, cancellationToken);

            if (establecimiento is null)
                throw new KeyNotFoundException($"Establecimiento con ID {id} no encontrado.");

            logger.LogInformation("Establecimiento encontrado: {Nombre}", establecimiento.Nombre);
            return new EstablecimientoDto
            {
                Id = establecimiento.Id,
                Nombre = establecimiento.Nombre,
                Direccion = establecimiento.Direccion,
                Telefono = establecimiento.Telefono,
                Email = establecimiento.Email
            };
        }
        catch (Exception)
        {
            logger.LogError("Error al obtener el establecimiento con ID: {EstablecimientoId}", id);
            throw;
        }
    }

    public async Task<EstablecimientoConCanchasDto> ObtenerPorIdConCanchaAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Obteniendo establecimiento con ID: {EstablecimientoId}", id);
            var establecimiento = await unitOfWork.GetRepository<Establecimiento, Guid>()
                .GetAsQueryable()
                .Include(e => e.Canchas)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            if (establecimiento is null)
                throw new KeyNotFoundException($"Establecimiento con ID {id} no encontrado.");

            logger.LogInformation("Establecimiento encontrado: {Nombre}", establecimiento.Nombre);
            return new EstablecimientoConCanchasDto
            {
                Id = establecimiento.Id,
                Nombre = establecimiento.Nombre,
                Direccion = establecimiento.Direccion,
                Telefono = establecimiento.Telefono,
                Email = establecimiento.Email,
                Canchas = establecimiento.Canchas.Select(c => new CanchaDto
                {
                    Id = c.Id,
                    Numero = c.Numero,
                    TipoSuelo = c.TipoSuelo,
                    TipoSueloParseado = c.TipoSuelo.AsString(),
                    EstablecimientoId = c.EstablecimientoId
                })
            };
        }
        catch (Exception)
        {
            logger.LogError("Error al obtener el establecimiento con ID: {EstablecimientoId}", id);
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<EstablecimientoDto>> ObtenerPaginadoAsync(GetItemsPaginated paginationInfo, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Obteniendo establecimientos paginados. Página: {PageNumber}, Tamaño: {PageSize}", paginationInfo.PageNumber, paginationInfo.PageSize);
            var establecimientos = await unitOfWork.GetRepository<Establecimiento, Guid>()
            .GetAllAsync(paginationInfo, cancellationToken);

            if (establecimientos is null || !establecimientos.Any())
            {
                logger.LogInformation("No se encontraron establecimientos para la página {PageNumber}.", paginationInfo.PageNumber);
                return Enumerable.Empty<EstablecimientoDto>();
            }

            logger.LogInformation("Se encontraron {Count} establecimientos en la página {PageNumber}.", establecimientos.Count(), paginationInfo.PageNumber);
            return establecimientos.Select(x => new EstablecimientoDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Direccion = x.Direccion,
                Telefono = x.Telefono,
                Email = x.Email
            });
        }
        catch (Exception)
        {
            logger.LogError("Error al obtener establecimientos paginados. Página: {PageNumber}, Tamaño: {PageSize}", paginationInfo.PageNumber, paginationInfo.PageSize);
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<EstablecimientoDto> ModificarAsync(ModificarEstablecimientoRequest establecimiento, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Iniciando el proceso de modificación del establecimiento con ID: {EstablecimientoId}", establecimiento.Id);

            var establecimientoExistente = await unitOfWork.GetRepository<Establecimiento, Guid>()
            .GetByIdAsync(establecimiento.Id, cancellationToken);

            establecimientoExistente.SetNombre(establecimiento.Nombre);
            establecimientoExistente.SetDireccion(establecimiento.Direccion);
            establecimientoExistente.SetTelefono(establecimiento.Telefono);
            establecimientoExistente.SetEmail(establecimiento.Email);

            unitOfWork.GetRepository<Establecimiento, Guid>().Update(establecimientoExistente);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            logger.LogInformation("Establecimiento modificado con éxito. ID: {EstablecimientoId}", establecimientoExistente.Id);
            return new EstablecimientoDto
            {
                Id = establecimientoExistente.Id,
                Nombre = establecimientoExistente.Nombre,
                Direccion = establecimientoExistente.Direccion,
                Telefono = establecimientoExistente.Telefono,
                Email = establecimientoExistente.Email
            };
        }
        catch (Exception)
        {
            logger.LogError("Error al modificar el establecimiento con ID: {EstablecimientoId}", establecimiento.Id);
            throw;
        }
        
    }

    public async Task EliminarAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Iniciando el proceso de eliminación del establecimiento con ID: {EstablecimientoId}", id);
            var establecimiento = await unitOfWork.GetRepository<Establecimiento, Guid>()
            .GetByIdAsync(id, cancellationToken);

            unitOfWork.GetRepository<Establecimiento, Guid>()
                .Delete(establecimiento);
            logger.LogInformation("Establecimiento eliminado: {Nombre}", establecimiento.Nombre);
        }
        catch (Exception)
        {
            logger.LogError("Error al eliminar el establecimiento con ID: {EstablecimientoId}", id);
            throw;
        }
    }
}
