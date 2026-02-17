namespace Usuarios.API.Domain.Entities;

public class Pai : Usuario
{
    protected Pai() { }

    public Pai(string nome, string email, string telefone)
        : base(nome, email, telefone)
    {
    }

    public ICollection<PaisFilhos> PaisFilhos { get; private set; } = new List<PaisFilhos>();
}
