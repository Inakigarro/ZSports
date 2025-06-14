using Microsoft.Extensions.Logging;
using ZSports.Contracts;
using ZSports.Establecimientos.Contracts.Establecimientos;
using ZSports.Establecimientos.Contracts.Establecimientos.CrearEstablecimiento;
using ZSports.Establecimientos.Contracts.Establecimientos.ModificarEstablecimiento;
using ZSports.Establecimientos.Domain;

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
        var establecimiento = await unitOfWork.GetRepository<Establecimiento, Guid>().GetByIdAsync(id, cancellationToken);
        return new EstablecimientoDto
        {
            Id = establecimiento.Id,
            Nombre = establecimiento.Nombre,
            Direccion = establecimiento.Direccion,
            Telefono = establecimiento.Telefono,
            Email = establecimiento.Email
        };
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<EstablecimientoDto>> ObtenerPaginadoAsync(GetItemsPaginated paginationInfo, CancellationToken cancellationToken)
    {
        var establecimientos = await unitOfWork.GetRepository<Establecimiento, Guid>()
            .GetAllAsync(paginationInfo, cancellationToken);

        return establecimientos.Select(x => new EstablecimientoDto()
        {
            Id = x.Id,
            Nombre = x.Nombre,
            Direccion = x.Direccion,
            Telefono = x.Telefono,
            Email = x.Email
        });
    }

    /// <inheritdoc/>
    public async Task<EstablecimientoDto> ModificarAsync(ModificarEstablecimientoRequest establecimiento, CancellationToken cancellationToken)
    {
        var establecimientoExistente = await unitOfWork.GetRepository<Establecimiento, Guid>()
            .GetByIdAsync(establecimiento.Id, cancellationToken);

        establecimientoExistente.SetNombre(establecimiento.Nombre);
        establecimientoExistente.SetDireccion(establecimiento.Direccion);
        establecimientoExistente.SetTelefono(establecimiento.Telefono);
        establecimientoExistente.SetEmail(establecimiento.Email);

        unitOfWork.GetRepository<Establecimiento, Guid>().Update(establecimientoExistente);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new EstablecimientoDto
        {
            Id = establecimientoExistente.Id,
            Nombre = establecimientoExistente.Nombre,
            Direccion = establecimientoExistente.Direccion,
            Telefono = establecimientoExistente.Telefono,
            Email = establecimientoExistente.Email
        };
    }

    public async Task EliminarAsync(Guid id, CancellationToken cancellationToken)
    {
        var establecimiento = await unitOfWork.GetRepository<Establecimiento, Guid>()
            .GetByIdAsync(id, cancellationToken);

        unitOfWork.GetRepository<Establecimiento, Guid>()
            .Delete(establecimiento);
    }
}
