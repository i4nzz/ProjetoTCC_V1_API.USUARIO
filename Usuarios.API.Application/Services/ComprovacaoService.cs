using System.Net;
using GestaoTarefas.Application.Common.Responses;
using GestaoTarefas.Application.DTOs.Pontuacao;
using GestaoTarefas.Application.DTOs.Recompensa;
using GestaoTarefas.Application.Interfaces;
using GestaoTarefas.Application.Mapping;
using GestaoTarefas.Domain.Entities;
using GestaoTarefas.Domain.Enum;
using GestaoTarefas.Domain.Interfaces;

namespace GestaoTarefas.Application.Services;

public class ComprovacaoService : IComprovacaoService
{
    private readonly IComprovacaoRepository _comprovacaoRepository;
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IPontuacaoService _pontuacaoService;
    private readonly IPontuacaoRepository _pontuacaoRepository;
    public ComprovacaoService(
        IComprovacaoRepository comprovacaoRepository
        , IPontuacaoRepository pontuacaoRepository
        , ITarefaRepository tarefaRepository
        , IPontuacaoService pontuacaoService
        )
    {
        _comprovacaoRepository = comprovacaoRepository;
        _pontuacaoService = pontuacaoService;
        _tarefaRepository = tarefaRepository;
        _pontuacaoRepository = pontuacaoRepository;
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

    public async Task<RespostaMetodos<RetornoComprovacaoDto>> ValidarAsync(int id, bool aprovar)
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

        var retornoTarefa = await _tarefaRepository.ObterPorIdAsync(comprovacao.TarefaId);

        if (retornoTarefa == null)
        {
            return new RespostaMetodos<RetornoComprovacaoDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Tarefa para essa validação não foi encontrada"
            };
        }

        var pontuacaoTarefa = new CriarPontuacaoDto()
        {
            FilhoId = retornoTarefa.FilhoId,
            TarefaId = retornoTarefa.TarefaId,
            Pontos = retornoTarefa.Pontos,
        };

        if (aprovar)
        {
            if (comprovacao.Status == StatusValidacaoTarefaEnum.Aprovada)
            {
                return new RespostaMetodos<RetornoComprovacaoDto>
                {
                    Sucesso = true,
                    ObjetoRetorno = comprovacao.ToDto(),
                    Mensagem = "Comprovação já estava aprovada"
                };
            }

            comprovacao.Aprovar();

            var existePontos = await _pontuacaoRepository.ExisteAsync(retornoTarefa.TarefaId, retornoTarefa.FilhoId);

            if (!existePontos)
            {
                await _pontuacaoService.AdicionarAsync(pontuacaoTarefa);
            }
        }
        else
        {
            if (comprovacao.Status == StatusValidacaoTarefaEnum.Reprovada)
            {
                return new RespostaMetodos<RetornoComprovacaoDto>
                {
                    Sucesso = true,
                    ObjetoRetorno = comprovacao.ToDto(),
                    Mensagem = "Comprovação já estava reprovada"
                };
            }

            if (comprovacao.Status == StatusValidacaoTarefaEnum.Aprovada)
            {
                return new RespostaMetodos<RetornoComprovacaoDto>
                {
                    Sucesso = false,
                    Mensagem = "Comprovação já foi aprovada e não pode ser reprovada"
                };
            }

            comprovacao.Reprovar();
        }
        await _comprovacaoRepository.AtualizarAsync(comprovacao);

        return new RespostaMetodos<RetornoComprovacaoDto>
        {
            Sucesso = true,
            ObjetoRetorno = comprovacao.ToDto(),
            Mensagem = aprovar ? "Comprovação aprovada com sucesso" : "Comprovação reprovada com sucesso"
        };
    }
}