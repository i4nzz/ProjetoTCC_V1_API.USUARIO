using Usuarios.API.Ioc;
using Usuarios.API.IoC;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(7018);
//});

builder.Services
    .AddDatabase(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddSwaggerConfiguration()
    .AddInfrastructure()
    .AddControllers();

var app = builder.Build();

app.UseStaticFiles();
app.UseSwaggerConfiguration();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseHttpsRedirection();
app.Run();