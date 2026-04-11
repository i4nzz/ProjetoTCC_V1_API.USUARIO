using Usuarios.API.Domain.Enum;

namespace Usuarios.API.Domain.Entities;

public class Pai : Usuario
{
    protected Pai() { }

    public Pai(string nome, string email, string senhaHash)
        : base(nome, email, senhaHash, PerfilUsuarioEnum.Pai)
    {
    }

    public ICollection<PaisFilhos> PaisFilhos { get; private set; } = new List<PaisFilhos>();
}