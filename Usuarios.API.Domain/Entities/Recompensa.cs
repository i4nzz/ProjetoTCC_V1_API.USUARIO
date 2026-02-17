namespace Usuarios.API.Domain.Entities;

public class Recompensa
{
    public int Id { get; private set; }

    public int FilhoId { get; private set; }
    public Filho Filho { get; private set; }

    public string Descricao { get; private set; }
    public int PontosNecessarios { get; private set; }
    public bool Ativa { get; private set; }

    protected Recompensa() { }

    public Recompensa(int filhoId, string descricao, int pontosNecessarios)
    {
        FilhoId = filhoId;
        Descricao = descricao;
        PontosNecessarios = pontosNecessarios;
        Ativa = true;
    }
}
