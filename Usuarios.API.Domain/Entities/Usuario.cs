using GestaoTarefas.Domain.Enum;

namespace GestaoTarefas.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
    public PerfilUsuarioEnum Perfil { get; set; }
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
