using Usuarios.API.Application.DTOs.Recompensa;

namespace Usuarios.API.Application.Interfaces;

public interface IComprovacaoService
{
    Task<IEnumerable<RetornoComprovacaoDto>> ObterPorTarefaAsync(int tarefaId);
    Task<RetornoComprovacaoDto?> ObterPorIdAsync(int id);
    Task<RetornoComprovacaoDto> EnviarAsync(CriarComprovacaoDto dto);
    Task<RetornoComprovacaoDto> ValidarAsync(int id);
}
