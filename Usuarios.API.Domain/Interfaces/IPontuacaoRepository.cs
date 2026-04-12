using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Domain.Interfaces;

public interface IPontuacaoRepository
{
    Task<IEnumerable<Pontuacao>> ObterPorFilhoAsync(int filhoId);
    Task<int> ObterTotalPontosAsync(int filhoId);
    Task AdicionarAsync(Pontuacao pontuacao);
}
