namespace Usuarios.API.Domain.Entities;

public class ComprovacaoTarefa
{
    public int Id { get; private set; }

    public int TarefaId { get; private set; }
    public Tarefa Tarefa { get; private set; }

    public string UrlFoto { get; private set; }
    public DateTime DataEnvio { get; private set; }
    public bool Validada { get; private set; }
    public DateTime? DataValidacao { get; private set; }

    protected ComprovacaoTarefa() { }

    public ComprovacaoTarefa(int tarefaId, string urlFoto)
    {
        TarefaId = tarefaId;
        UrlFoto = urlFoto;
        DataEnvio = DateTime.UtcNow;
        Validada = false;
    }

    public void Validar()
    {
        Validada = true;
        DataValidacao = DateTime.UtcNow;
    }
}
