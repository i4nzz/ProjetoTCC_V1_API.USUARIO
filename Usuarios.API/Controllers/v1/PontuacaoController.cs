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
    public async Task<IActionResult> ObterPorFilho(int filhoId)
    {
        var pontuacoes = await _pontuacaoService.ObterPorFilhoAsync(filhoId);

        if (pontuacoes == null || !pontuacoes.Any())
            return NotFound(ResponseApi<RetornoPontuacaoDto>.Erro("Nenhuma pontuação encontrada para este filho"));

        return Ok(new ResponseApi<IEnumerable<RetornoPontuacaoDto>>(pontuacoes, "Pontuações obtidas com sucesso"));
    }

    [HttpGet]
    [Route("ObterTotal/{filhoId}")]
    public async Task<IActionResult> ObterTotal(int filhoId)
    {
        var total = await _pontuacaoService.ObterTotalPontosAsync(filhoId);

        if (total == 0)
            return NoContent();

        return Ok(new ResponseApi<RetornoPontuacaoDto>(new RetornoPontuacaoDto { Pontos = total }, "Total de pontos obtido com sucesso"));
    }

    [HttpPost]
    [Route("Adicionar")]
    public async Task<IActionResult> Adicionar([FromBody] CriarPontuacaoDto dto)
    {
        var pontuacao = await _pontuacaoService.AdicionarAsync(dto);
        return Ok(new ResponseApi<RetornoPontuacaoDto>(pontuacao, "Pontuação adicionada com sucesso"));
    }
}
