using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZSports.Contracts;
using ZSports.Domain;

namespace ZSports.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RolesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly RoleManager<Role> _roleManager;

    public RolesController(IUnitOfWork unitOfWork, RoleManager<Role> roleManager)
    {
        _unitOfWork = unitOfWork;
        _roleManager = roleManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(string roleName, CancellationToken cancellationToken)
    {
        var role = new Role
        {
            Name = roleName,
            NormalizedName = roleName.ToUpperInvariant(),
        };

        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
    }

    [HttpGet("{roleName}")]
    public async Task<IActionResult> GetRole(string roleName, CancellationToken cancellationToken)
    {
        var role = await _roleManager.Roles
            .Where(r => !string.IsNullOrEmpty(r.Name) && r.Name.Equals(roleName))
            .FirstOrDefaultAsync(cancellationToken);

        if (role == null) return NotFound();
        return Ok(role);
    }
}
