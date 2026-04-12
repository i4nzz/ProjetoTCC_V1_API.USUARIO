using Usuarios.API.Application.Interfaces;
using Usuarios.API.Application.Services;
using Usuarios.API.Domain.Interfaces;
using Usuarios.API.Infra.Repositories;
using Usuarios.API.Infrastructure.Repositories;

namespace Usuarios.API.Ioc;

public static class ConfiguracaoInjecaoDeDependencia
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        #region Repositories
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ITarefaRepository, TarefaRepository>();
        services.AddScoped<IPontuacaoRepository, PontuacaoRepository>();
        services.AddScoped<IRecompensaRepository, RecompensaRepository>();
        #endregion

        #region Services
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ITarefaService, TarefaService>();
        services.AddScoped<IPontuacaoService, PontuacaoService>();
        services.AddScoped<IRecompensaService, RecompensaService>();
        #endregion

        return services;
    }
}