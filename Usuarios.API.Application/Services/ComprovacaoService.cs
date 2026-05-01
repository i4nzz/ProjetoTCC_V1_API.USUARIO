using System.Net;
using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Recompensa;
using Usuarios.API.Application.Interfaces;
using Usuarios.API.Application.Mapping;
using Usuarios.API.Domain.Entities;
using Usuarios.API.Domain.Interfaces;

namespace Usuarios.API.Application.Services;

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

        return new RespostaMetodos<IEnumerable<RetornoComprovacaoDto>>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = comprovacoes.Select(x => new RetornoComprovacaoDto()
            {
                TarefaId = x.TarefaId,
                TituloTarefa = x.Tarefa.Titulo ?? string.Empty,
                UrlFoto = x.UrlFoto,
                Validada = x.Validada,
                DataEnvio = x.DataEnvio,
                DataValidacao = x.DataValidacao
            })
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
