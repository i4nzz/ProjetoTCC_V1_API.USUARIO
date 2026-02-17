namespace Usuarios.API.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateTime DataCriacao { get; set; }

    protected Usuario() { }
    public Usuario(string nome, string email, string telefone)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        DataCriacao = DateTime.UtcNow;
    }
}
