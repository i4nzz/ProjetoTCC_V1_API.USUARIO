using Microsoft.EntityFrameworkCore;
using GestaoTarefas.Infra.Data;

namespace GestaoTarefas.IoC;

public static class ConfiguracaoBaseDeDados
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContexto>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        return services;
    }
}
