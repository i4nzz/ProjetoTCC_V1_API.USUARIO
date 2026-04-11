using Usuarios.API.Application.DTOs.Usuario;
namespace Usuarios.API.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<RetornoUsuarioDto>> ObterTodosAsync();
        Task<RetornoUsuarioDto?> ObterPorIdAsync(int id);
        Task<RetornoUsuarioDto> CriarAsync(CriarUsuarioDto dto);
        Task AtualizarAsync(int id, CriarUsuarioDto dto);
        Task RemoverAsync(int id);
    }
}
