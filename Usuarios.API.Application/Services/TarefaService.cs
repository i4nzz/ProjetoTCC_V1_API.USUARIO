using Usuarios.API.Application.DTOs.Tarefa;
using Usuarios.API.Application.Interfaces;
using Usuarios.API.Domain.Entities;
using Usuarios.API.Domain.Interfaces;

namespace Usuarios.API.Application.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _repository;

    public TarefaService(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RetornoTarefaDto>> ObterTodasAsync()
    {
        var tarefas = await _repository.ObterTodasAsync();
        return tarefas.Select(MapToDto);
    }

    public async Task<IEnumerable<RetornoTarefaDto>> ObterPorFilhoAsync(int filhoId)
    {
        var tarefas = await _repository.ObterPorFilhoAsync(filhoId);
        return tarefas.Select(MapToDto);
    }

    public async Task<RetornoTarefaDto?> ObterPorIdAsync(int tarefaId)
    {
        var tarefa = await _repository.ObterPorIdAsync(tarefaId);
        return tarefa == null ? null : MapToDto(tarefa);
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

        await _repository.AdicionarAsync(tarefa);
        return MapToDto(tarefa);
    }

    public async Task AtualizarAsync(int tarefaId, CriarTarefaDto dto)
    {
        var tarefa = await _repository.ObterPorIdAsync(tarefaId);
        if (tarefa == null)
            throw new KeyNotFoundException("Tarefa não encontrada");

        tarefa.Titulo = dto.Titulo;
        tarefa.Descricao = dto.Descricao;
        tarefa.Pontos = dto.Pontos;
        tarefa.Prazo = dto.Prazo;

        await _repository.AtualizarAsync(tarefa);
    }

    public async Task RemoverAsync(int tarefaId)
    {
        var tarefa = await _repository.ObterPorIdAsync(tarefaId);
        if (tarefa == null)
            throw new KeyNotFoundException("Tarefa não encontrada");

        await _repository.RemoverAsync(tarefaId);
    }

    private static RetornoTarefaDto MapToDto(Tarefa tarefa)
    {
        return new RetornoTarefaDto
        {
            TarefaId = tarefa.TarefaId,
            FilhoId = tarefa.FilhoId,
            NomeFilho = tarefa.Filho?.Nome ?? string.Empty,
            Titulo = tarefa.Titulo,
            Descricao = tarefa.Descricao,
            Pontos = tarefa.Pontos,
            Prazo = tarefa.Prazo,
            DataCriacao = tarefa.DataCriacao
        };
    }
}