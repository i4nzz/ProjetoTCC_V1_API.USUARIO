using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Login;
using Usuarios.API.Application.DTOs.Usuario;
namespace Usuarios.API.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<RespostaMetodos<IEnumerable<RetornoUsuarioDto>>> ObterTodosAsync();
        Task<RespostaMetodos<RetornoUsuarioDto?>> ObterPorIdAsync(int id);
        Task<RespostaMetodos<RetornoUsuarioDto>> CriarUsuarioAsync(CriarUsuarioDto dto);
        Task<RespostaMetodos<RetornoUsuarioDto>> AtualizarAsync(int id, CriarUsuarioDto dto);
        Task<RespostaMetodos<RetornoUsuarioDto>> RemoverAsync(int id);
        Task<RespostaMetodos<RetornoUsuarioDto>> CriarFilhoAsync(CriarFilhoDto dto);
        Task<RespostaMetodos<RetornoLoginDto>> LoginAsync(LoginDto dto);
    }
}
