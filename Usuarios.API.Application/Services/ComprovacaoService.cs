using Usuarios.API.Application.DTOs.Recompensa;
using Usuarios.API.Application.Interfaces;
using Usuarios.API.Application.Mapping;
using Usuarios.API.Domain.Entities;
using Usuarios.API.Domain.Interfaces;

namespace Usuarios.API.Application.Services;

public class ComprovacaoService : IComprovacaoService
{
    private readonly IComprovacaoRepository _repository;

    public ComprovacaoService(IComprovacaoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RetornoComprovacaoDto>> ObterPorTarefaAsync(int tarefaId)
    {
        var comprovacoes = await _repository.ObterPorTarefaAsync(tarefaId);
        return comprovacoes.ToDtoList();
    }

    public async Task<RetornoComprovacaoDto?> ObterPorIdAsync(int id)
    {
        var comprovacao = await _repository.ObterPorIdAsync(id);
        return comprovacao?.ToDto();
    }

    public async Task<RetornoComprovacaoDto> EnviarAsync(CriarComprovacaoDto dto)
    {
        var comprovacao = new ComprovacaoTarefa(dto.TarefaId, dto.UrlFoto);
        await _repository.AdicionarAsync(comprovacao);
        return comprovacao.ToDto();
    }

    public async Task<RetornoComprovacaoDto> ValidarAsync(int id)
    {
        var comprovacao = await _repository.ObterPorIdAsync(id);
        if (comprovacao == null)
            throw new KeyNotFoundException("Comprovação não encontrada");

        if (comprovacao.Validada)
            throw new InvalidOperationException("Comprovação já foi validada");

        comprovacao.Validar();
        await _repository.AtualizarAsync(comprovacao);
        return comprovacao.ToDto();
    }
}
