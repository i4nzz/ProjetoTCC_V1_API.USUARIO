namespace Usuarios.API.Domain.Entities;

public class Recompensa
{
    public int Id { get; set; }
    public int FilhoId { get; set; }
    public int TarefaId { get; set; }
    public string? Descricao { get; set; }
    public int PontosNecessarios { get; set; }
    public bool PodeNecessariosInt { get; set; }
    public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
    public Usuario Filho { get; set; } = null!;
}
