using System.Net;
using GestaoTarefas.Application.Common.Responses;
using GestaoTarefas.Application.DTOs.Recompensa;
using GestaoTarefas.Application.Interfaces;
using GestaoTarefas.Application.Mapping;
using GestaoTarefas.Domain.Entities;
using GestaoTarefas.Domain.Interfaces;

namespace GestaoTarefas.Application.Services;

public class RecompensaService : IRecompensaService
{
    private readonly IRecompensaRepository _recompensaRepository;
    private readonly IPontuacaoRepository _pontuacaoRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public RecompensaService(
        IRecompensaRepository recompensaRepository
        , IPontuacaoRepository pontuacaoRepository
        , IUsuarioRepository usuarioRepository
        )
    {
        _recompensaRepository = recompensaRepository;
        _pontuacaoRepository = pontuacaoRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<RespostaMetodos<IEnumerable<RetornoRecompensaDto>>> ObterPorFilhoAsync(int filhoId)
    {
        var recompensas = await _recompensaRepository.ObterPorFilhoAsync(filhoId);

        if (recompensas == null || !recompensas.Any())
        {
            return new RespostaMetodos<IEnumerable<RetornoRecompensaDto>>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Nenhuma recompensa encontrada para este filho."
            };
        }

        var retornoRecompensas = recompensas.ToDtoList();

        return new RespostaMetodos<IEnumerable<RetornoRecompensaDto>>
        {
            Sucesso = true,
            ObjetoRetorno = retornoRecompensas,
            StatusCode = HttpStatusCode.OK,
            Mensagem = "Recompensas obtidas com sucesso."
        };
    }

    public async Task<RespostaMetodos<RetornoRecompensaDto?>> ObterPorIdAsync(int id)
    {
        var recompensa = await _recompensaRepository.ObterPorIdAsync(id);

        if (recompensa == null)
        {
            return new RespostaMetodos<RetornoRecompensaDto?>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Recompensa não encontrada."
            };
        }

        var retornoRecompensa = recompensa.ToDto();

        return new RespostaMetodos<RetornoRecompensaDto?>
        {
            Sucesso = true,
            ObjetoRetorno = retornoRecompensa,
            StatusCode = HttpStatusCode.OK,
            Mensagem = "Recompensa obtida com sucesso."
        };
    }

    public async Task<RespostaMetodos<RetornoRecompensaDto>> CriarAsync(CriarRecompensaDto dto)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(dto.FilhoId);

        if (usuario == null)
        {
            return new RespostaMetodos<RetornoRecompensaDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Filho não encontrado."
            };
        }

        if (dto.PontosNecessarios <= 0)
        {
            return new RespostaMetodos<RetornoRecompensaDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Pontos necessários devem ser maiores que zero."
            };
        }

        var recompensa = new Recompensa
        {
            FilhoId = dto.FilhoId,
            Descricao = dto.Descricao,
            PontosNecessarios = dto.PontosNecessarios,
            Ativa = true
        };

        await _recompensaRepository.AdicionarAsync(recompensa);

        var retornoRecompensa = recompensa.ToDto();

        return new RespostaMetodos<RetornoRecompensaDto>
        {
            Sucesso = true,
            ObjetoRetorno = retornoRecompensa,
            StatusCode = HttpStatusCode.Created,
            Mensagem = "Recompensa criada com sucesso."
        };
    }

    public async Task<RespostaMetodos<RetornoRecompensaDto>> AtualizarAsync(int id, CriarRecompensaDto dto)
    {
        var recompensa = await _recompensaRepository.ObterPorIdAsync(id);

        if (recompensa == null)
        {
            return new RespostaMetodos<RetornoRecompensaDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Recompensa não encontrada."
            };
        }

        recompensa.Descricao = dto.Descricao;
        recompensa.PontosNecessarios = dto.PontosNecessarios;

        await _recompensaRepository.AtualizarAsync(recompensa);
        var retornoRecompensa = recompensa.ToDto();

        return new RespostaMetodos<RetornoRecompensaDto>
        {
            Sucesso = true,
            ObjetoRetorno = retornoRecompensa,
            StatusCode = HttpStatusCode.OK,
            Mensagem = "Recompensa atualizada com sucesso."
        };
    }

    public async Task<RespostaMetodos<RetornoRecompensaDto>> RemoverAsync(int id)
    {
        var recompensa = await _recompensaRepository.ObterPorIdAsync(id);

        if (recompensa == null)
        {
            return new RespostaMetodos<RetornoRecompensaDto>
            {
                Sucesso = false,
                Mensagem = "Recompensa não encontrada."
            };
        }

        await _recompensaRepository.RemoverAsync(id);

        return new RespostaMetodos<RetornoRecompensaDto>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            Mensagem = "Recompensa removida com sucesso."
        };
    }

    public async Task<RespostaMetodos<RetornoRecompensaResgatadaDto>> ResgatarAsync(int filhoId, int recompensaId)
    {
        var recompensa = await _recompensaRepository.ObterPorIdAsync(recompensaId);

        if (recompensa == null)
        {
            return new RespostaMetodos<RetornoRecompensaResgatadaDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Recompensa não encontrada."
            };
        }

        if (!recompensa.Ativa)
        {
            return new RespostaMetodos<RetornoRecompensaResgatadaDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Recompensa não está ativa."
            };
        }

        var totalPontos = await _pontuacaoRepository.ObterTotalPontosAsync(filhoId);

        if (totalPontos < recompensa.PontosNecessarios)
        {
            return new RespostaMetodos<RetornoRecompensaResgatadaDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Pontos insuficientes para resgatar esta recompensa."
            };
        }

        var resgatada = new RecompensaResgatada(filhoId, recompensaId);
        await _recompensaRepository.ResgatarAsync(resgatada);
        await _pontuacaoRepository.DebitarPontosAsync(filhoId, recompensa.PontosNecessarios);

        var retornoResgatada = resgatada.ToDto();

        return new RespostaMetodos<RetornoRecompensaResgatadaDto>
        {
            Sucesso = true,
            ObjetoRetorno = retornoResgatada,
            StatusCode = HttpStatusCode.OK,
            Mensagem = "Recompensa resgatada com sucesso."
        };
    }

    public async Task<RespostaMetodos<IEnumerable<RetornoRecompensaResgatadaDto>>> ObterResgatadasPorFilhoAsync(int filhoId)
    {
        var resgatadas = await _recompensaRepository.ObterResgatadasPorFilhoAsync(filhoId);

        if (resgatadas == null || !resgatadas.Any())
        {
            return new RespostaMetodos<IEnumerable<RetornoRecompensaResgatadaDto>>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Nenhuma recompensa resgatada encontrada para este filho."
            };
        }

        var retornoResgatadas = resgatadas.ToDtoListResgatadas();

        return new RespostaMetodos<IEnumerable<RetornoRecompensaResgatadaDto>>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = retornoResgatadas,
            Mensagem = "Recompensas resgatadas obtidas com sucesso."
        };
    }
}
