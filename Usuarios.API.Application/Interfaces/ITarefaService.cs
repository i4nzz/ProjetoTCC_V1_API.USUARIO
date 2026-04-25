using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Tarefa;

namespace Usuarios.API.Application.Interfaces;

public interface ITarefaService
{
    Task<RespostaMetodos<IEnumerable<RetornoTarefaDto>>> ObterTodasAsync();
    Task<RespostaMetodos<IEnumerable<RetornoTarefaDto>>> ObterPorFilhoAsync(int filhoId);
    Task<RespostaMetodos<RetornoTarefaDto?>> ObterPorIdAsync(int tarefaId);
    Task<RespostaMetodos<RetornoTarefaDto>> CriarAsync(CriarTarefaDto dto);
    Task<RespostaMetodos<RetornoTarefaDto?>> AtualizarAsync(int tarefaId, CriarTarefaDto dto);
    Task<RespostaMetodos<RetornoTarefaDto?>> RemoverAsync(int tarefaId);
}
