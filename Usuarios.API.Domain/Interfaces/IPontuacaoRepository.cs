using GestaoTarefas.Domain.Entities;

namespace GestaoTarefas.Domain.Interfaces;

public interface IPontuacaoRepository
{
    Task<IEnumerable<Pontuacao>> ObterPorFilhoAsync(int filhoId);
    Task<int> ObterTotalPontosAsync(int filhoId);
    Task AdicionarAsync(Pontuacao pontuacao);
}
