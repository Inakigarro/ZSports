using Microsoft.Extensions.DependencyInjection;
using ZSports.Establecimientos.Contracts.Canchas;
using ZSports.Establecimientos.Contracts.Establecimientos;
using ZSports.Establecimientos.Persistence;

namespace ZSports.Establecimientos.Application;

public static class Dependencias
{
    public static IServiceCollection AgregarDependenciasEstablecimientos(this IServiceCollection services)
    {
        // Registro de servicios específicos de la aplicación de establecimientos
        services
            .AddScoped<IEstablecimientosService, EstablecimientosService>()
            .AddScoped<ICanchasService, CanchasService>();
        
        return services;
    }
}
