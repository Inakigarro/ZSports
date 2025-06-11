using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZSports.Contracts;
using ZSports.Domain;

namespace ZSports.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RolesController(RoleManager<Role> roleManager) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateRole(string roleName, CancellationToken cancellationToken)
    {
        var role = new Role
        {
            Name = roleName,
            NormalizedName = roleName.ToUpperInvariant(),
        };

        var result = await roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
    }

    [HttpGet("{roleName}")]
    public async Task<IActionResult> GetRole(string roleName, CancellationToken cancellationToken)
    {
        var role = await roleManager.Roles
            .Where(r => !string.IsNullOrEmpty(r.Name) && r.Name.Equals(roleName))
            .FirstOrDefaultAsync(cancellationToken);

        if (role == null) return NotFound();
        return Ok(role);
    }
}
