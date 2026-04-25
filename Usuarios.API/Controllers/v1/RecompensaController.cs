using System.Net;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> ObterPorFilho(int filhoId)
    {
        var recompensas = await _recompensaService.ObterPorFilhoAsync(filhoId);

        if (!recompensas.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, recompensas);
        }

        return StatusCode((int)HttpStatusCode.OK, recompensas);
    }

    [HttpGet]
    [Route("ObterPorId/{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var recompensa = await _recompensaService.ObterPorIdAsync(id);

        if (!recompensa.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, recompensa);
        }

        return StatusCode((int)HttpStatusCode.OK, recompensa);

    }

    [HttpPost]
    [Route("Criar")]
    public async Task<IActionResult> Criar([FromBody] CriarRecompensaDto dto)
    {
        var recompensa = await _recompensaService.CriarAsync(dto);

        if (!recompensa.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, recompensa);
        }

        return StatusCode((int)HttpStatusCode.Created, recompensa);
    }

    [HttpPut]
    [Route("Atualizar/{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] CriarRecompensaDto dto)
    {
        var atualizado = await _recompensaService.AtualizarAsync(id, dto);

        if (!atualizado.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, atualizado);
        }

        return StatusCode((int)HttpStatusCode.OK, atualizado);
    }

    [HttpDelete]
    [Route("Remover/{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        var removido = await _recompensaService.RemoverAsync(id);

        if (!removido.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, removido);
        }

        return StatusCode((int)HttpStatusCode.OK, removido);
    }

    [HttpPost]
    [Route("Resgatar/{filhoId}/{recompensaId}")]
    public async Task<IActionResult> Resgatar(int filhoId, int recompensaId)
    {
        var resgatada = await _recompensaService.ResgatarAsync(filhoId, recompensaId);

        if (!resgatada.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, resgatada);
        }

        return StatusCode((int)HttpStatusCode.OK, resgatada);
    }

    [HttpGet]
    [Route("ObterResgatadas/{filhoId}")]
    public async Task<IActionResult> ObterResgatadas(int filhoId)
    {
        var resgatadas = await _recompensaService.ObterResgatadasPorFilhoAsync(filhoId);

        if (!resgatadas.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, resgatadas);
        }

        return StatusCode((int)HttpStatusCode.OK, resgatadas);
    }


}