using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Recompensa;

namespace Usuarios.API.Application.Interfaces;

public interface IRecompensaService
{
    Task<RespostaMetodos<IEnumerable<RetornoRecompensaDto>>> ObterPorFilhoAsync(int filhoId);
    Task<RespostaMetodos<RetornoRecompensaDto?>> ObterPorIdAsync(int id);
    Task<RespostaMetodos<RetornoRecompensaDto>> CriarAsync(CriarRecompensaDto dto);
    Task<RespostaMetodos<RetornoRecompensaDto>> AtualizarAsync(int id, CriarRecompensaDto dto);
    Task<RespostaMetodos<RetornoRecompensaDto>> RemoverAsync(int id);
    Task<RespostaMetodos<RetornoRecompensaResgatadaDto>> ResgatarAsync(int filhoId, int recompensaId);
    Task<RespostaMetodos<IEnumerable<RetornoRecompensaResgatadaDto>>> ObterResgatadasPorFilhoAsync(int filhoId);
}
