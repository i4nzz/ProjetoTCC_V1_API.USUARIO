using Microsoft.EntityFrameworkCore;
using Usuarios.API.Infrastructure.Data;
using Usuarios.API.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

// liberar a porta 
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7018); // HTTP
});

// Add services to the container.
builder.Services.AddDbContext<UsuarioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfrastructure();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// somente http
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
