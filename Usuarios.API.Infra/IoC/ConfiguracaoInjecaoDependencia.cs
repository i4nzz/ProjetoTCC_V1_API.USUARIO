using Microsoft.Extensions.DependencyInjection;
using Usuarios.API.Application.Interfaces;
using Usuarios.API.Application.Services;
using Usuarios.API.Domain.Repositories;
using Usuarios.API.Infrastructure.Repositories;

namespace Usuarios.API.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        
        return services;
    }
}