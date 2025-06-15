using Microsoft.AspNetCore.Mvc;
using ZSports.Contracts;
using ZSports.Establecimientos.Contracts.Establecimientos;
using ZSports.Establecimientos.Contracts.Establecimientos.CrearEstablecimiento;

namespace ZSports.Establecimientos.Application;

[ApiController]
[Route("[controller]")]
public class EstablecimientosController(IEstablecimientosService service): ControllerBase
{
    [HttpPost]
    [Route("crearEstablecimiento")]
    public async Task<IActionResult> CrearEstablecimiento(CrearEstablecimientoRequest request, CancellationToken cancellationToken = default)
    {
		try
		{
            var establecimientoCreado = await service.AgregarAsync(request, cancellationToken);
            return Ok(establecimientoCreado);
        }
		catch (Exception)
		{
            return BadRequest($"Ocurrio un error durante el proceso de creacion del establecimiento {request.Nombre}");
		}
    }

    [HttpGet]
    [Route("obtenerEstablecimientos")]
    public async Task<IActionResult> ObtenerEstablecimientos(GetItemsPaginated paginationInfo, CancellationToken cancellationToken = default)
    {
        try
        {
            var establecimientos = await service.ObtenerPaginadoAsync(paginationInfo, cancellationToken);
            return Ok(establecimientos);
        }
        catch (Exception)
        {
            return BadRequest("Ocurrio un error durante el proceso de obtencion de los establecimientos");
        }
    }

    [HttpGet]
    [Route("obtenerEstablecimiento")]
    public async Task<IActionResult> ObtenerEstablecimiento([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var establecimiento = await service.ObtenerPorIdAsync(id, cancellationToken);
            if (establecimiento is null)
                return NotFound($"No se encontro el establecimiento con ID {id}");
            return Ok(establecimiento);
        }
        catch (Exception)
        {
            return BadRequest("Ocurrio un error durante el proceso de obtencion del establecimiento");
        }
    }

    [HttpGet]
    [Route("obtenerEstablecimientoConCancha")]
    public async Task<IActionResult> ObtenerEstablecimientoConCancha([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var establecimiento = await service.ObtenerPorIdConCanchaAsync(id, cancellationToken);
            if (establecimiento is null)
                return NotFound($"No se encontro el establecimiento con ID {id}");
            return Ok(establecimiento);
        }
        catch (Exception)
        {
            return BadRequest("Ocurrio un error durante el proceso de obtencion del establecimiento");
        }
    }
}
