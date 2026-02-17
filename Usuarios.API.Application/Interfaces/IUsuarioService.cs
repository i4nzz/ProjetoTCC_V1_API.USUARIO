using Usuarios.API.Application.DTOs;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> ObterTodosAsync();
        Task<Usuario?> ObterPorIdAsync(int id);
        Task<UsuarioDto> CriarAsync(UsuarioDto dto);
        Task AtualizarAsync(int id, UsuarioDto dto);
        Task RemoverAsync(int id);
        //Task GerarHashSenha(UsuarioDto dto);
    }
}
