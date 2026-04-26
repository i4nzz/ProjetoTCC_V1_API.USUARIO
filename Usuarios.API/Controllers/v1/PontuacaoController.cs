using System.Net;
using Microsoft.AspNetCore.Mvc;
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

    /// <summary>
    /// Obter as pontuações de um filho específico.
    /// </summary>
    /// <param name="filhoId">ID do filho</param>
    /// <returns>Lista de pontuações do filho</returns>
    [HttpGet]
    [Route("ObterPorFilho/{filhoId}")]
    public async Task<IActionResult> ObterPorFilho(int filhoId)
    {
        var pontuacoes = await _pontuacaoService.ObterPorFilhoAsync(filhoId);

        if (!pontuacoes.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.NoContent, pontuacoes);
        }

        return StatusCode((int)HttpStatusCode.OK, pontuacoes.ObjetoRetorno);
    }

    /// <summary>
    /// Obter o total de pontos acumulados por um filho específico.
    /// </summary>
    /// <param name="filhoId">ID do filho</param>
    /// <returns>Total de pontos do filho</returns>
    [HttpGet]
    [Route("ObterTotal/{filhoId}")]
    public async Task<IActionResult> ObterTotal(int filhoId)
    {
        var total = await _pontuacaoService.ObterTotalPontosAsync(filhoId);

        if (!total.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.NoContent, total.Mensagem);
        }

        return StatusCode((int)HttpStatusCode.OK, total.ObjetoRetorno);
    }

    /// <summary>
    /// Adicionar uma nova pontuação para um filho.
    /// </summary>
    /// <param name="dto">Dados da pontuação</param>
    /// <returns>Resultado da operação</returns>
    [HttpPost]
    [Route("Adicionar")]
    public async Task<IActionResult> Adicionar([FromBody] CriarPontuacaoDto dto)
    {
        var pontuacao = await _pontuacaoService.AdicionarAsync(dto);

        if (!pontuacao.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, pontuacao.Mensagem);
        }

        return StatusCode((int)HttpStatusCode.Created, pontuacao.ObjetoRetorno);
    }
}