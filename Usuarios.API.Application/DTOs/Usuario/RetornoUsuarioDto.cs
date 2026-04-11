using Usuarios.API.Domain.Enum;

namespace Usuarios.API.Application.DTOs.Usuario;

public class RetornoUsuarioDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public PerfilUsuarioEnum Perfil { get; set; }
    public bool Ativo { get; set; }
}