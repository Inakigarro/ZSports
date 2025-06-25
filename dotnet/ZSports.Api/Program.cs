using Microsoft.EntityFrameworkCore;
using ZSports.Persistence;
using ZSports.Domain;
using Microsoft.AspNetCore.Identity;
using ZSports.Contracts;
using ZSports.Establecimientos.Contracts;
using ZSports.Establecimientos.Persistence;
using ZSports.Establecimientos.Application;

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

        builder.Services
            .AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AgregarDependenciasEstablecimientos();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularLocalhost",
                policy => policy
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
            );
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowAngularLocalhost");
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}