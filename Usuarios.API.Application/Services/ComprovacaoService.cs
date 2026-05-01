using System.Net;
using GestaoTarefas.Application.Common.Responses;
using GestaoTarefas.Application.DTOs.Recompensa;
using GestaoTarefas.Application.Interfaces;
using GestaoTarefas.Application.Mapping;
using GestaoTarefas.Domain.Entities;
using GestaoTarefas.Domain.Interfaces;

namespace GestaoTarefas.Application.Services;

public class ComprovacaoService : IComprovacaoService
{
    private readonly IComprovacaoRepository _comprovacaoRepository;

    public ComprovacaoService(IComprovacaoRepository comprovacaoRepository)
    {
        _comprovacaoRepository = comprovacaoRepository;
    }

    public async Task<RespostaMetodos<IEnumerable<RetornoComprovacaoDto>>> ObterPorTarefaAsync(int tarefaId)
    {
        var comprovacoes = await _comprovacaoRepository.ObterPorTarefaAsync(tarefaId);

        if (comprovacoes == null || !comprovacoes.Any())
        {
            return new RespostaMetodos<IEnumerable<RetornoComprovacaoDto>>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = $"Nenhuma comprovação encontrada para a tarefa {tarefaId}"
            };
        }
        var retorno = comprovacoes.ToDtoList();
        return new RespostaMetodos<IEnumerable<RetornoComprovacaoDto>>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = retorno
        };
    }

    public async Task<RespostaMetodos<RetornoComprovacaoDto?>> ObterPorIdAsync(int id)
    {
        var comprovacao = await _comprovacaoRepository.ObterPorIdAsync(id);

        if (comprovacao == null)
        {
            return new RespostaMetodos<RetornoComprovacaoDto?>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Comprovação não encontrada"
            };
        }
        var retorno = comprovacao.ToDto();

        return new RespostaMetodos<RetornoComprovacaoDto?>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = retorno
        };
    }

    public async Task<RespostaMetodos<RetornoComprovacaoDto>> EnviarAsync(CriarComprovacaoDto dto)
    {
        if (dto == null || dto.UrlFoto == null || dto.TarefaId <= 0)
        {
            return new RespostaMetodos<RetornoComprovacaoDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Dados vazios ou inválidos"
            };
        }

        var comprovacao = new ComprovacaoTarefa(dto.TarefaId, dto.UrlFoto);

        await _comprovacaoRepository.AdicionarAsync(comprovacao);

        var retornoComprovacao = comprovacao.ToDto();

        return new RespostaMetodos<RetornoComprovacaoDto>
        {
            Sucesso = true,
            ObjetoRetorno = retornoComprovacao
        };
    }

    public async Task<RespostaMetodos<RetornoComprovacaoDto>> ValidarAsync(int id)
    {
        var comprovacao = await _comprovacaoRepository.ObterPorIdAsync(id);

        if (comprovacao == null)
        {
            return new RespostaMetodos<RetornoComprovacaoDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Comprovação não encontrada"
            };
        }

        if (comprovacao.Validada)
        {
            return new RespostaMetodos<RetornoComprovacaoDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Comprovação já foi validada"
            };
        }

        comprovacao.Validar();
        await _comprovacaoRepository.AtualizarAsync(comprovacao);

        var retornoComprovacao = comprovacao.ToDto();

        return new RespostaMetodos<RetornoComprovacaoDto>
        {
            Sucesso = true,
            ObjetoRetorno = retornoComprovacao,
            Mensagem = "Comprovação validada com sucesso"
        };
    }
}
