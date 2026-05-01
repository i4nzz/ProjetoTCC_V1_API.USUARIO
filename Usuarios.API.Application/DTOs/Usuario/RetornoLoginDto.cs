namespace GestaoTarefas.Application.DTOs.Usuario;

public class RetornoLoginDto
{
    public string Token { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Perfil { get; set; } = string.Empty;
    public DateTime Expiracao { get; set; }
}
