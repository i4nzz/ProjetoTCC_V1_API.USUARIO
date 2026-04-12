using Usuarios.API.Application.DTOs.Pontuacao;
using Usuarios.API.Application.Interfaces;
using Usuarios.API.Application.Mapping;
using Usuarios.API.Domain.Entities;
using Usuarios.API.Domain.Interfaces;
namespace Usuarios.API.Application.Services;

public class PontuacaoService : IPontuacaoService
{
    private readonly IPontuacaoRepository _pontuacaoRepository;

    public PontuacaoService(IPontuacaoRepository pontuacaoRepository)
    {
        _pontuacaoRepository = pontuacaoRepository;
    }

    public async Task<IEnumerable<RetornoPontuacaoDto>> ObterPorFilhoAsync(int filhoId)
    {
        var pontuacoes = await _pontuacaoRepository.ObterPorFilhoAsync(filhoId);
        return pontuacoes.ToDtoList();
    }

    public async Task<int> ObterTotalPontosAsync(int filhoId)
    {
        return await _pontuacaoRepository.ObterTotalPontosAsync(filhoId);
    }

    public async Task<RetornoPontuacaoDto> AdicionarAsync(CriarPontuacaoDto dto)
    {
        var pontuacao = new Pontuacao
        {
            FilhoId = dto.FilhoId,
            TarefaId = dto.TarefaId,
            Pontos = dto.Pontos
        };

        await _pontuacaoRepository.AdicionarAsync(pontuacao);
        return pontuacao.ToDto();
    }
}
