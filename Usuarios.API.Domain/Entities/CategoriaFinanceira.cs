namespace Usuarios.API.Domain.Entities;

public class CategoriaFinanceira
{
    public int Id { get; private set; }

    public string Nome { get; private set; }

    protected CategoriaFinanceira() { }

    public CategoriaFinanceira(string nome)
    {
        Nome = nome;
    }
    // dotnet ef migrations add secondCreate --project Usuarios.API.Infra --startup-project Usuarios.API
    // dotnet ef database update --project Usuarios.API.Infra --startup-project Usuarios.API
}
