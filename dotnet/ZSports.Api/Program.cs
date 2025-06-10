using Microsoft.EntityFrameworkCore;
using ZSports.Persistence;
using ZSports.Domain;
using Microsoft.AspNetCore.Identity;

namespace ZSports.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        // Add DbContext and Identity
        builder.Services.AddDbContext<ZSportsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ZSportsDbContext>()
            .AddDefaultTokenProviders();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}