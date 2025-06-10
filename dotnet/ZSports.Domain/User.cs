namespace ZSports.Domain;

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    public string? FullName { get; set; }
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}
