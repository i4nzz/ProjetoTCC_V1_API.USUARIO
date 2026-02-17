namespace Usuarios.API.Domain.Entities;

public class Tarefa
{
    public int Id { get; private set; }

    public int FilhoId { get; private set; }
    public Filho Filho { get; private set; }

    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public int Pontos { get; private set; }
    public DateTime Prazo { get; private set; }
    public string Status { get; private set; }
    public DateTime DataCriacao { get; private set; }

    protected Tarefa() { }

    public Tarefa(int filhoId, string titulo, string descricao, int pontos, DateTime prazo)
    {
        FilhoId = filhoId;
        Titulo = titulo;
        Descricao = descricao;
        Pontos = pontos;
        Prazo = prazo;
        Status = "Pendente";
        DataCriacao = DateTime.UtcNow;
    }
}
