using Usuarios.API.Domain.Enum;

namespace Usuarios.API.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
    public PerfilUsuarioEnum Perfil { get; set; }
    public int? PaiId { get; set; }
    public Usuario? Pai { get; set; }
    public ICollection<Usuario> Filhos { get; set; } = new List<Usuario>();

    protected Usuario() { }

    public Usuario(string nome, string email, string senhaHash, PerfilUsuarioEnum perfil)
    {
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Perfil = perfil;
        Ativo = true;
    }
}
