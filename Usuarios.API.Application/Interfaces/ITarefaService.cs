using Usuarios.API.Application.DTOs.Tarefa;

namespace Usuarios.API.Application.Interfaces;

public interface ITarefaService
{
    Task<IEnumerable<RetornoTarefaDto>> ObterTodasAsync();
    Task<IEnumerable<RetornoTarefaDto>> ObterPorFilhoAsync(int filhoId);
    Task<RetornoTarefaDto?> ObterPorIdAsync(int tarefaId);
    Task<RetornoTarefaDto> CriarAsync(CriarTarefaDto dto);
    Task AtualizarAsync(int tarefaId, CriarTarefaDto dto);
    Task RemoverAsync(int tarefaId);
}
