using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Pontuacao;

namespace Usuarios.API.Application.Interfaces;

public interface IPontuacaoService
{
    Task<RespostaMetodos<IEnumerable<RetornoPontuacaoDto>>> ObterPorFilhoAsync(int filhoId);
    Task<RespostaMetodos<int>> ObterTotalPontosAsync(int filhoId);
    Task<RespostaMetodos<RetornoPontuacaoDto>> AdicionarAsync(CriarPontuacaoDto dto);
}
