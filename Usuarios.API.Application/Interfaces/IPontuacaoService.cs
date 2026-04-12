using Usuarios.API.Application.DTOs.Pontuacao;

namespace Usuarios.API.Application.Interfaces;

public interface IPontuacaoService
{
    Task<IEnumerable<RetornoPontuacaoDto>> ObterPorFilhoAsync(int filhoId);
    Task<int> ObterTotalPontosAsync(int filhoId);
    Task<RetornoPontuacaoDto> AdicionarAsync(CriarPontuacaoDto dto);
}
