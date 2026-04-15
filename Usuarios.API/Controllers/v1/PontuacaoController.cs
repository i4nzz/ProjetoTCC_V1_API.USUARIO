using System.Net;
using Microsoft.AspNetCore.Mvc;
using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Pontuacao;
using Usuarios.API.Application.Interfaces;

namespace Usuarios.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class PontuacaoController : ControllerBase
{
    private readonly IPontuacaoService _pontuacaoService;

    public PontuacaoController(IPontuacaoService pontuacaoService)
    {
        _pontuacaoService = pontuacaoService;
    }

    [HttpGet]
    [Route("ObterPorFilho/{filhoId}")]
    public async Task<RespostaMetodos<IEnumerable<RetornoPontuacaoDto>>> ObterPorFilho(int filhoId)
    {
        var pontuacoes = await _pontuacaoService.ObterPorFilhoAsync(filhoId);

        if (pontuacoes == null || !pontuacoes.Any())
        {
            return new RespostaMetodos<IEnumerable<RetornoPontuacaoDto>>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NoContent,
                ObjetoRetorno = null,
                Mensagem = "Nenhuma pontuação encontrada"
            };
        }

        return new RespostaMetodos<IEnumerable<RetornoPontuacaoDto>>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = pontuacoes
        };
    }

    [HttpGet]
    [Route("ObterTotal/{filhoId}")]
    public async Task<RespostaMetodos<RetornoPontuacaoDto>> ObterTotal(int filhoId)
    {
        var total = await _pontuacaoService.ObterTotalPontosAsync(filhoId);

        if (total == 0)
        {
            return new RespostaMetodos<RetornoPontuacaoDto>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NoContent,
                ObjetoRetorno = null,
                Mensagem = "Nenhum ponto encontrado"
            };
        }

        return new RespostaMetodos<RetornoPontuacaoDto>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = new RetornoPontuacaoDto { Pontos = total }
        };
    }

    [HttpPost]
    [Route("Adicionar")]
    public async Task<RespostaMetodos<RetornoPontuacaoDto>> Adicionar([FromBody] CriarPontuacaoDto dto)
    {
        var pontuacao = await _pontuacaoService.AdicionarAsync(dto);

        return new RespostaMetodos<RetornoPontuacaoDto>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.Created,
            ObjetoRetorno = pontuacao
        };
    }
}