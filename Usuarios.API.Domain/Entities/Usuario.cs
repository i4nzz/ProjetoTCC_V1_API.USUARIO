namespace Usuarios.API.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string SenhaHash { get; set; } = "$2b$12$HashFakePadraoSoParaTeste12345678901234567890";
    public DateTime DataCriacao { get; set; }
    public bool Ativo { get; set; } = true;

    protected Usuario() { }
    public Usuario(string nome, string email, string telefone)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        DataCriacao = DateTime.Now;
    }
}
