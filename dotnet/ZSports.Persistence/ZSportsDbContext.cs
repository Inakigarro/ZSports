using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZSports.Domain;
using ZSports.Persistence.Configurations;

namespace ZSports.Persistence;

public class ZSportsDbContext : IdentityDbContext<User, Role, Guid>
{
    public ZSportsDbContext(DbContextOptions<ZSportsDbContext> options) : base(options) { }

    public DbSet<Permission> Permissions => Set<Permission>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new PermissionConfiguration());
    }
}
