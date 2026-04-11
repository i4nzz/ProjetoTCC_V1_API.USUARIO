using Usuarios.API.Domain.Enum;

namespace Usuarios.API.Application.DTOs.Usuario;

public class CriarUsuarioDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public PerfilUsuarioEnum Perfil { get; set; }
}
