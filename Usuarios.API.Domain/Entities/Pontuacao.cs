namespace Usuarios.API.Domain.Entities;

public class Pontuacao
{
    public int Id { get; private set; }

    public int FilhoId { get; private set; }
    public Filho Filho { get; private set; }

    public int TarefaId { get; private set; }
    public Tarefa Tarefa { get; private set; }

    public int Pontos { get; private set; }
    public DateTime DataRegistro { get; private set; }

    protected Pontuacao() { }

    public Pontuacao(int filhoId, int tarefaId, int pontos)
    {
        FilhoId = filhoId;
        TarefaId = tarefaId;
        Pontos = pontos;
        DataRegistro = DateTime.UtcNow;
    }
}
