using Microsoft.EntityFrameworkCore;
using Usuarios.API.Infra.Data;

namespace Usuarios.API.IoC;

public static class ConfiguracaoBaseDeDados
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContexto>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        return services;
    }
}
