using Microsoft.AspNetCore.Mvc;
using ZSports.Establecimientos.Contracts.Canchas;
using ZSports.Establecimientos.Contracts.Canchas.CrearCancha;

namespace ZSports.Establecimientos.Application;

[ApiController]
[Route("[controller]")]
public class CanchasController(ICanchasService canchasService): ControllerBase
{
    [HttpPost]
    [Route("agregarCancha")]
    public async Task<IActionResult> AgregarCancha(CrearCanchaRequest request, CancellationToken cancellationToken = default)
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
}
