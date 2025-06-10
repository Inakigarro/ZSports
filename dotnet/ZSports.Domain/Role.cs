namespace ZSports.Domain;

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

public class Role : IdentityRole<Guid>
{
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
