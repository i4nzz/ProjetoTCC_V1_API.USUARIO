using System.Net;
using Microsoft.AspNetCore.Mvc;
using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Recompensa;
using Usuarios.API.Application.Interfaces;

namespace Usuarios.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class RecompensaController : ControllerBase
{
    private readonly IRecompensaService _recompensaService;

    public RecompensaController(IRecompensaService recompensaService)
    {
        _recompensaService = recompensaService;
    }

    [HttpGet]
    [Route("ObterPorFilho/{filhoId}")]
    public async Task<RespostaMetodos<IEnumerable<RetornoRecompensaDto>>> ObterPorFilho(int filhoId)
    {
        var recompensas = await _recompensaService.ObterPorFilhoAsync(filhoId);

        if (recompensas == null || !recompensas.Any())
        {
            return new RespostaMetodos<IEnumerable<RetornoRecompensaDto>>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NoContent,
                ObjetoRetorno = null,
                Mensagem = $"Nenhuma recompensa encontrada para o filhoId {filhoId}."
            };
        }

        return new RespostaMetodos<IEnumerable<RetornoRecompensaDto>>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = recompensas,
            Mensagem = $"Recompensas encontradas para o filhoId {filhoId}."
        };
    }

    [HttpGet]
    [Route("ObterPorId/{id}")]
    public async Task<RespostaMetodos<RetornoRecompensaDto>> ObterPorId(int id)
    {
        var recompensa = await _recompensaService.ObterPorIdAsync(id);

        if (recompensa == null)
        {
            return new RespostaMetodos<RetornoRecompensaDto>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                ObjetoRetorno = null,
                Mensagem = "Recompensa não encontrada"
            };
        }

        return new RespostaMetodos<RetornoRecompensaDto>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = recompensa,
            Mensagem = "Recompensa obtida com sucesso"
        };
    }

    [HttpPost]
    [Route("Criar")]
    public async Task<RespostaMetodos<RetornoRecompensaDto>> Criar([FromBody] CriarRecompensaDto dto)
    {
        var recompensa = await _recompensaService.CriarAsync(dto);

        return new RespostaMetodos<RetornoRecompensaDto>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.Created,
            ObjetoRetorno = recompensa,
            Mensagem = "Recompensa criada com sucesso"
        };
    }

    [HttpPut]
    [Route("Atualizar/{id}")]
    public async Task<RespostaMetodos<string>> Atualizar(int id, [FromBody] CriarRecompensaDto dto)
    {
        await _recompensaService.AtualizarAsync(id, dto);

        return new RespostaMetodos<string>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = "OK",
            Mensagem = "Recompensa atualizada com sucesso"
        };
    }

    [HttpDelete]
    [Route("Remover/{id}")]
    public async Task<RespostaMetodos<string>> Remover(int id)
    {
        await _recompensaService.RemoverAsync(id);

        return new RespostaMetodos<string>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = "OK",
            Mensagem = "Recompensa removida com sucesso"
        };
    }

    [HttpPost]
    [Route("Resgatar/{filhoId}/{recompensaId}")]
    public async Task<RespostaMetodos<RetornoRecompensaResgatadaDto>> Resgatar(int filhoId, int recompensaId)
    {
        var resgatada = await _recompensaService.ResgatarAsync(filhoId, recompensaId);

        return new RespostaMetodos<RetornoRecompensaResgatadaDto>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = resgatada,
            Mensagem = "Recompensa resgatada com sucesso"
        };
    }

    [HttpGet]
    [Route("ObterResgatadas/{filhoId}")]
    public async Task<RespostaMetodos<IEnumerable<RetornoRecompensaResgatadaDto>>> ObterResgatadas(int filhoId)
    {
        var resgatadas = await _recompensaService.ObterResgatadasPorFilhoAsync(filhoId);

        return new RespostaMetodos<IEnumerable<RetornoRecompensaResgatadaDto>>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = resgatadas,
            Mensagem = "Recompensas resgatadas obtidas com sucesso"
        };
    }
}