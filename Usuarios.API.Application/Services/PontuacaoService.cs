using GestaoTarefas.Application.Common.Responses;
using GestaoTarefas.Application.DTOs.Pontuacao;
using GestaoTarefas.Application.Interfaces;
using GestaoTarefas.Application.Mapping;
using GestaoTarefas.Domain.Entities;
using GestaoTarefas.Domain.Interfaces;
namespace GestaoTarefas.Application.Services;

public class PontuacaoService : IPontuacaoService
{
    private readonly IPontuacaoRepository _pontuacaoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITarefaRepository _tarefaRepository;

    public PontuacaoService(
        IPontuacaoRepository pontuacaoRepository
        , IUsuarioRepository usuarioRepository
        , ITarefaRepository tarefaRepository
        )
    {
        _pontuacaoRepository = pontuacaoRepository;
        _usuarioRepository = usuarioRepository;
        _tarefaRepository = tarefaRepository;
    }

    public async Task<RespostaMetodos<IEnumerable<RetornoPontuacaoDto>>> ObterPorFilhoAsync(int filhoId)
    {
        var pontuacoes = await _pontuacaoRepository.ObterPorFilhoAsync(filhoId);

        if (pontuacoes == null || !pontuacoes.Any())
        {
            return new RespostaMetodos<IEnumerable<RetornoPontuacaoDto>>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Nenhuma pontuação encontrada"
            };
        }
        var retornoPontuacoes = pontuacoes.Select(p => p.ToDto()).ToList();

        return new RespostaMetodos<IEnumerable<RetornoPontuacaoDto>>
        {
            Sucesso = true,
            ObjetoRetorno = retornoPontuacoes,
            Mensagem = "Pontuações encontradas"
        };
    }

    public async Task<RespostaMetodos<int>> ObterTotalPontosAsync(int filhoId)
    {
        var totalPontos = await _pontuacaoRepository.ObterTotalPontosAsync(filhoId);

        if (totalPontos <= 0)
        {
            return new RespostaMetodos<int>
            {
                Sucesso = false,
                ObjetoRetorno = 0,
                Mensagem = "Nenhum ponto encontrado"
            };
        }

        return new RespostaMetodos<int>
        {
            Sucesso = true,
            ObjetoRetorno = totalPontos,
            Mensagem = $"Pontos encontrados para o filhoId {filhoId}"
        };
    }

    public async Task<RespostaMetodos<RetornoPontuacaoDto>> AdicionarAsync(CriarPontuacaoDto dto)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(dto.FilhoId);

        if (usuario == null)
        {
            return new RespostaMetodos<RetornoPontuacaoDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Usuário não encontrado"
            };
        }

        var tarefa = await _tarefaRepository.ObterPorIdAsync(dto.TarefaId);

        if (tarefa == null)
        {
            return new RespostaMetodos<RetornoPontuacaoDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Tarefa não encontrada"
            };
        }

        var pontuacao = Pontuacao.CriarGanho(dto.FilhoId, dto.TarefaId, dto.Pontos);

        await _pontuacaoRepository.AdicionarAsync(pontuacao);

        var retornoPontuacao = pontuacao.ToDto();

        return new RespostaMetodos<RetornoPontuacaoDto>
        {
            Sucesso = true,
            ObjetoRetorno = retornoPontuacao,
            Mensagem = "Pontuação adicionada com sucesso"
        };
    }
}
