using Microsoft.Extensions.Logging;
using ZSports.Contracts;
using ZSports.Establecimientos.Contracts.Canchas;
using ZSports.Establecimientos.Contracts.Canchas.CrearCancha;
using ZSports.Establecimientos.Contracts.Canchas.ModificarCancha;
using ZSports.Establecimientos.Domain;

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
                EstablecimientoId = nuevaCancha.EstablecimientoId
            };
        }
        catch (Exception ex)
        {
            logger.LogError("Error al crear la cancha. Detalles: {ExceptionMessage}", ex.Message);
            throw;
        }
    }

    public Task EliminarAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<CanchaDto> ModificarAsync(ModificarCanchaRequest cancha, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CanchaDto>> ObtenerPaginadoAsync(GetItemsPaginated paginationInfo, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<CanchaDto> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
