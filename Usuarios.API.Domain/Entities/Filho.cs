using Usuarios.API.Domain.Enum;

namespace Usuarios.API.Domain.Entities;

public class Filho : Usuario
{
    public DateTime DataNascimento { get; private set; }
    public ICollection<PaisFilhos> PaisFilhos { get; private set; } = new List<PaisFilhos>();

    protected Filho() { }

    public Filho(string nome, string email, string senhaHash, DateTime dataNascimento)
        : base(nome, email, senhaHash, PerfilUsuarioEnum.Filho)
    {
        DataNascimento = dataNascimento;
    }
}