namespace Usuarios.API.Application.DTOs
{
    public class UsuarioDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        public bool Ativo { get; set; }
    }
}
