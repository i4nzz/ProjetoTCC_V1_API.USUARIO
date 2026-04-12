using Usuarios.API.Application.DTOs.Tarefa;
using Usuarios.API.Application.Interfaces;
using Usuarios.API.Application.Mapping;
using Usuarios.API.Domain.Entities;
using Usuarios.API.Domain.Interfaces;

namespace Usuarios.API.Application.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;

    public TarefaService(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    public async Task<IEnumerable<RetornoTarefaDto>> ObterTodasAsync()
    {
        var tarefas = await _tarefaRepository.ObterTodasAsync();
        return tarefas.ToDtoList();
    }

    public async Task<IEnumerable<RetornoTarefaDto>> ObterPorFilhoAsync(int filhoId)
    {
        var tarefas = await _tarefaRepository.ObterPorFilhoAsync(filhoId);
        return tarefas.ToDtoList();
    }

    public async Task<RetornoTarefaDto?> ObterPorIdAsync(int tarefaId)
    {
        var tarefa = await _tarefaRepository.ObterPorIdAsync(tarefaId);
        return tarefa?.ToDto();
    }

    public async Task<RetornoTarefaDto> CriarAsync(CriarTarefaDto dto)
    {
        var tarefa = new Tarefa
        {
            FilhoId = dto.FilhoId,
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Pontos = dto.Pontos,
            Prazo = dto.Prazo
        };

        await _tarefaRepository.AdicionarAsync(tarefa);
        return tarefa.ToDto();
    }

    public async Task AtualizarAsync(int tarefaId, CriarTarefaDto dto)
    {
        var tarefa = await _tarefaRepository.ObterPorIdAsync(tarefaId);
        if (tarefa == null)
            throw new KeyNotFoundException("Tarefa não encontrada");

        tarefa.Titulo = dto.Titulo;
        tarefa.Descricao = dto.Descricao;
        tarefa.Pontos = dto.Pontos;
        tarefa.Prazo = dto.Prazo;

        await _tarefaRepository.AtualizarAsync(tarefa);
    }

    public async Task RemoverAsync(int tarefaId)
    {
        var tarefa = await _tarefaRepository.ObterPorIdAsync(tarefaId);
        if (tarefa == null)
            throw new KeyNotFoundException("Tarefa não encontrada");

        await _tarefaRepository.RemoverAsync(tarefaId);
    }
}