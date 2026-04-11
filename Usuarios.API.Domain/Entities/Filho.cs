using Usuarios.API.Domain.Enum;

namespace Usuarios.API.Domain.Entities;

public class Filho : Usuario
{
    public DateTime DataNascimento { get; private set; }

    protected Filho() { }

    public Filho(string nome, string email, string senhaHash, DateTime dataNascimento)
        : base(nome, email, senhaHash, PerfilUsuarioEnum.Filho)
    {
        DataNascimento = dataNascimento;
    }
}