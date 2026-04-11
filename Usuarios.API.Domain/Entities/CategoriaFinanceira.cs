namespace Usuarios.API.Domain.Entities;

public class CategoriaFinanceira
{
    public int CategoriaFinanceiraId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public ICollection<RegistroFinanceiro> Registros { get; set; } = new List<RegistroFinanceiro>();
}
// dotnet ef migrations add newCreate --project Usuarios.API.Infra --startup-project Usuarios.API
// dotnet ef database update --project Usuarios.API.Infra --startup-project Usuarios.API
