using Usuarios.API.Application.DTOs.Recompensa;

namespace Usuarios.API.Application.Interfaces;

public interface IRecompensaService
{
    Task<IEnumerable<RetornoRecompensaDto>> ObterPorFilhoAsync(int filhoId);
    Task<RetornoRecompensaDto?> ObterPorIdAsync(int id);
    Task<RetornoRecompensaDto> CriarAsync(CriarRecompensaDto dto);
    Task AtualizarAsync(int id, CriarRecompensaDto dto);
    Task RemoverAsync(int id);
    Task<RetornoRecompensaResgatadaDto> ResgatarAsync(int filhoId, int recompensaId);
    Task<IEnumerable<RetornoRecompensaResgatadaDto>> ObterResgatadasPorFilhoAsync(int filhoId);
}
