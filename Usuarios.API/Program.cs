using Microsoft.EntityFrameworkCore;
using Usuarios.API.Infra.Data;
using Usuarios.API.Ioc;

var builder = WebApplication.CreateBuilder(args);

// liberar a porta 
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7018); // HTTP
});

// Add services to the container.
builder.Services.AddDbContext<AppDbContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfrastructure();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.InjectStylesheet("https://cdn.jsdelivr.net/npm/swagger-ui-themes@3.0.0/themes/3.x/theme-flattop.css");
});

app.UseAuthorization();
app.MapControllers();

app.Run();