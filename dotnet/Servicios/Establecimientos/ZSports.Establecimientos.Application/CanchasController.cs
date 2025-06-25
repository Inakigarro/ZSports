using Microsoft.AspNetCore.Mvc;
using ZSports.Contracts;
using ZSports.Establecimientos.Contracts.Canchas;
using ZSports.Establecimientos.Contracts.Canchas.CrearCancha;
using ZSports.Establecimientos.Contracts.Canchas.ModificarCancha;

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
	public async Task<IActionResult> ObtenerCanchaPorId([FromQuery] Guid canchaId, CancellationToken cancellationToken = default)
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

	[HttpPut]
	[Route("editarCancha")]
	public async Task<IActionResult> EditarCancha([FromBody] ModificarCanchaRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			var canchaEditada = await canchasService.ModificarAsync(request, cancellationToken);
			return Ok(canchaEditada);
		}
		catch (Exception)
		{
			return BadRequest($"Ocurrio un error durante el proceso de edicion de la cancha numero: {request.Id}");
		}
	}

	[HttpDelete]
	[Route("eliminarCancha")]
	public async Task<IActionResult> EliminarCancha([FromQuery] Guid canchaId, CancellationToken cancellationToken = default)
	{
		try
		{
			await canchasService.EliminarAsync(canchaId, cancellationToken);
			return Ok(new { Message = $"La cancha con Id: {canchaId} fue eliminada correctamente." });
		}
		catch (KeyNotFoundException)
		{
			return NotFound($"No se encontro una cancha con Id: {canchaId}");
		}
		catch (Exception)
		{
			return BadRequest($"Ocurrio un error al eliminar la cancha con Id: {canchaId}");
		}
    }
}
