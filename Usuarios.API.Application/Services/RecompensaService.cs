using Usuarios.API.Application.DTOs.Recompensa;
using Usuarios.API.Application.Interfaces;
using Usuarios.API.Application.Mapping;
using Usuarios.API.Domain.Entities;
using Usuarios.API.Domain.Interfaces;

namespace Usuarios.API.Application.Services;

public class RecompensaService : IRecompensaService
{
    private readonly IRecompensaRepository _recompensaRepository;
    private readonly IPontuacaoRepository _pontuacaoRepository;

    public RecompensaService(IRecompensaRepository recompensaRepository, IPontuacaoRepository pontuacaoRepository)
    {
        _recompensaRepository = recompensaRepository;
        _pontuacaoRepository = pontuacaoRepository;
    }

    public async Task<IEnumerable<RetornoRecompensaDto>> ObterPorFilhoAsync(int filhoId)
    {
        var recompensas = await _recompensaRepository.ObterPorFilhoAsync(filhoId);
        return recompensas.ToDtoList();
    }

    public async Task<RetornoRecompensaDto?> ObterPorIdAsync(int id)
    {
        var recompensa = await _recompensaRepository.ObterPorIdAsync(id);
        return recompensa?.ToDto();
    }

    public async Task<RetornoRecompensaDto> CriarAsync(CriarRecompensaDto dto)
    {
        var recompensa = new Recompensa
        {
            FilhoId = dto.FilhoId,
            Descricao = dto.Descricao,
            PontosNecessarios = dto.PontosNecessarios,
            Ativa = true
        };

        await _recompensaRepository.AdicionarAsync(recompensa);
        return recompensa.ToDto();
    }

    public async Task AtualizarAsync(int id, CriarRecompensaDto dto)
    {
        var recompensa = await _recompensaRepository.ObterPorIdAsync(id);

        if (recompensa == null)
            throw new KeyNotFoundException("Recompensa não encontrada");

        recompensa.Descricao = dto.Descricao;
        recompensa.PontosNecessarios = dto.PontosNecessarios;

        await _recompensaRepository.AtualizarAsync(recompensa);
    }

    public async Task RemoverAsync(int id)
    {
        var recompensa = await _recompensaRepository.ObterPorIdAsync(id);

        if (recompensa == null)
            throw new KeyNotFoundException("Recompensa não encontrada");

        await _recompensaRepository.RemoverAsync(id);
    }

    public async Task<RetornoRecompensaResgatadaDto> ResgatarAsync(int filhoId, int recompensaId)
    {
        var recompensa = await _recompensaRepository.ObterPorIdAsync(recompensaId);

        if (recompensa == null)
            throw new KeyNotFoundException("Recompensa não encontrada");

        if (!recompensa.Ativa)
            throw new InvalidOperationException("Recompensa não está disponível");

        var totalPontos = await _pontuacaoRepository.ObterTotalPontosAsync(filhoId);

        if (totalPontos < recompensa.PontosNecessarios)
            throw new InvalidOperationException("Pontos insuficientes para resgatar esta recompensa");

        var resgatada = new RecompensaResgatada(filhoId, recompensaId);
        await _recompensaRepository.ResgatarAsync(resgatada);
        return resgatada.ToDto();
    }

    public async Task<IEnumerable<RetornoRecompensaResgatadaDto>> ObterResgatadasPorFilhoAsync(int filhoId)
    {
        var resgatadas = await _recompensaRepository.ObterResgatadasPorFilhoAsync(filhoId);
        return resgatadas.ToDtoListResgatadas();
    }
}
