using GestaoTarefas.Application.Interfaces;
using GestaoTarefas.Application.Services;
using GestaoTarefas.Domain.Interfaces;
using GestaoTarefas.Infra.Repositories;
using GestaoTarefas.Infrastructure.Repositories;

namespace GestaoTarefas.Ioc;

public static class ConfiguracaoInjecaoDeDependencia
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        #region Services
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ITarefaService, TarefaService>();
        services.AddScoped<IPontuacaoService, PontuacaoService>();
        services.AddScoped<IRecompensaService, RecompensaService>();
        services.AddScoped<IComprovacaoService, ComprovacaoService>();
        services.AddScoped<ITokenService, TokenService>();
        #endregion

        #region Repositories
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ITarefaRepository, TarefaRepository>();
        services.AddScoped<IPontuacaoRepository, PontuacaoRepository>();
        services.AddScoped<IRecompensaRepository, RecompensaRepository>();
        services.AddScoped<IComprovacaoRepository, ComprovacaoRepository>();
        #endregion

        return services;
    }
}