using GestaoTarefas.API.Application.Interfaces;
using GestaoTarefas.API.Application.Services;
using GestaoTarefas.API.Domain.Interfaces;
using GestaoTarefas.API.Infra.Repositories;
using GestaoTarefas.Application.Interfaces;
using GestaoTarefas.Application.Services;
using GestaoTarefas.Domain.Interfaces;
using GestaoTarefas.Infra.Repositories;
using GestaoTarefas.Infrastructure.Repositories;

namespace GestaoTarefas.Ioc;

/// <summary>
/// Classe para configurar a injeção de dependência dos serviços e repositórios da aplicação, registrando as implementações concretas para as interfaces correspondentes. Essa configuração é essencial para garantir que as dependências sejam resolvidas corretamente em tempo de execução, permitindo a inversão de controle e facilitando a manutenção e testabilidade do código.
/// </summary>
public static class ConfiguracaoInjecaoDeDependencia
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region Services
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ITarefaService, TarefaService>();
        services.AddScoped<IPontuacaoService, PontuacaoService>();
        services.AddScoped<IRecompensaService, RecompensaService>();
        services.AddScoped<IComprovacaoService, ComprovacaoService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, EmailService>();
        #endregion

        #region Repositories
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ITarefaRepository, TarefaRepository>();
        services.AddScoped<IPontuacaoRepository, PontuacaoRepository>();
        services.AddScoped<IRecompensaRepository, RecompensaRepository>();
        services.AddScoped<IComprovacaoRepository, ComprovacaoRepository>();
        services.AddScoped<IResgatePontuacaoRepository, ResgatePontuacaoRepository>();
        #endregion

        return services;
    }
}