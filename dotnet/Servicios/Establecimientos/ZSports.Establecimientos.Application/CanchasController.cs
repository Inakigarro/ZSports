using Microsoft.AspNetCore.Mvc;
using ZSports.Contracts;
using ZSports.Establecimientos.Contracts.Canchas;
using ZSports.Establecimientos.Contracts.Canchas.CrearCancha;

namespace ZSports.Establecimientos.Application;

[ApiController]
[Route("[controller]")]
public class CanchasController(ICanchasService canchasService): ControllerBase
{
    [HttpPost]
    [Route("agregarCancha")]
    public async Task<IActionResult> AgregarCancha([FromBody] CrearCanchaRequest request, CancellationToken cancellationToken = default)
    {
		try
		{
			var canchaCreada = await canchasService.AgregarAsync(request, cancellationToken);
			return Ok(canchaCreada);
        }
		catch (Exception)
		{
			return BadRequest("Ocurrio un error durante la creacion de la cancha.");
		}
    }

	[HttpGet]
	[Route("obtenerCanchasPorEstablecimiento")]
	public async Task<IActionResult> ObtenerCanchasPorEstablecimiento([FromQuery] Guid establecimientoId, CancellationToken cancellationToken = default)
	{
		try
		{
			var canchas = await canchasService
				.ObtenerPorEstablecimientoAsync(
					establecimientoId,
					new GetItemsPaginated
					{
						PageSize = 100,
						PageNumber = 1
					},
					cancellationToken);

			return Ok(canchas);
		}
		catch (Exception)
		{
			return BadRequest("Ocurrio un error al obtener las canchas del establecimiento.");
		}
    }

	[HttpGet]
	[Route("obtenerCanchaPorId")]
	public async Task<IActionResult> ObtenerCanchaPorId([FromQuery] Guid canchaId, CancellationToken cancellationToken)
	{
		try
		{
			var cancha = await canchasService.ObtenerPorIdAsync(canchaId, cancellationToken);
			return Ok(cancha);
		}
		catch (KeyNotFoundException)
		{
			return NotFound($"No se encontro una cancha con Id: {canchaId}");
        }
		catch (Exception)
		{
			return BadRequest($"Ocurrio un error al obtener la cancha con Id: {canchaId}");
		}
	}
}
