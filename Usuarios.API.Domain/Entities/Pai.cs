namespace Usuarios.API.Domain.Entities;
using Enum;
public class Pai : Usuario
{
    protected Pai() { }

    public Pai(string nome, string email, string telefone)
        : base(nome, email, telefone, Perfil.Pai)
    {
    }

    public ICollection<PaisFilhos> PaisFilhos { get; private set; } = new List<PaisFilhos>();
}
