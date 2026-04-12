using Microsoft.AspNetCore.Mvc;
using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Recompensa;
using Usuarios.API.Application.Interfaces;

namespace Usuarios.API.Controllers.v1;
[ApiController]
[Route("api/v1/[controller]")]
public class RecompensaController : ControllerBase
{
    private readonly IRecompensaService recompensaService;

    public RecompensaController(IRecompensaService recompensaService)
    {
        this.recompensaService = recompensaService;
    }

    [HttpGet]
    [Route("ObterPorFilho/{filhoId}")]
    public async Task<IActionResult> ObterPorFilho(int filhoId)
    {
        var recompensas = await recompensaService.ObterPorFilhoAsync(filhoId);
        return Ok(new ResponseApi<IEnumerable<RetornoRecompensaDto>>(recompensas, "Recompensas obtidas com sucesso"));
    }

    [HttpGet]
    [Route("ObterPorId/{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var recompensa = await recompensaService.ObterPorIdAsync(id);
        if (recompensa == null)
            return NotFound(ResponseApi<RetornoRecompensaDto>.Erro("Recompensa não encontrada"));

        return Ok(new ResponseApi<RetornoRecompensaDto>(recompensa, "Recompensa obtida com sucesso"));
    }

    [HttpPost]
    [Route("Criar")]
    public async Task<IActionResult> Criar([FromBody] CriarRecompensaDto dto)
    {
        var recompensa = await recompensaService.CriarAsync(dto);
        return Ok(new ResponseApi<RetornoRecompensaDto>(recompensa, "Recompensa criada com sucesso"));
    }

    [HttpPut]
    [Route("Atualizar/{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] CriarRecompensaDto dto)
    {
        await recompensaService.AtualizarAsync(id, dto);
        return Ok(new ResponseApi<string>("Recompensa atualizada com sucesso"));
    }

    [HttpDelete]
    [Route("Remover/{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        await recompensaService.RemoverAsync(id);
        return Ok(new ResponseApi<string>("Recompensa removida com sucesso"));
    }

    [HttpPost]
    [Route("Resgatar/{filhoId}/{recompensaId}")]
    public async Task<IActionResult> Resgatar(int filhoId, int recompensaId)
    {
        var resgatada = await recompensaService.ResgatarAsync(filhoId, recompensaId);
        return Ok(new ResponseApi<RetornoRecompensaResgatadaDto>(resgatada, "Recompensa resgatada com sucesso"));
    }

    [HttpGet]
    [Route("ObterResgatadas/{filhoId}")]
    public async Task<IActionResult> ObterResgatadas(int filhoId)
    {
        var resgatadas = await recompensaService.ObterResgatadasPorFilhoAsync(filhoId);
        return Ok(new ResponseApi<IEnumerable<RetornoRecompensaResgatadaDto>>(resgatadas, "Recompensas resgatadas obtidas com sucesso"));
    }
}
