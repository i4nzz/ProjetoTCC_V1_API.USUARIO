using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Recompensa;

namespace Usuarios.API.Application.Interfaces;

public interface IComprovacaoService
{
    Task<RespostaMetodos<IEnumerable<RetornoComprovacaoDto>>> ObterPorTarefaAsync(int tarefaId);
    Task<RespostaMetodos<RetornoComprovacaoDto?>> ObterPorIdAsync(int id);
    Task<RespostaMetodos<RetornoComprovacaoDto>> EnviarAsync(CriarComprovacaoDto dto);
    Task<RespostaMetodos<RetornoComprovacaoDto>> ValidarAsync(int id);
}
