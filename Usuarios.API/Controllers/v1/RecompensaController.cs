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

    /// <summary>
    /// Obtém todas as recompensas associadas a um filho específico.
    /// </summary>
    /// <param name="filhoId">ID do filho</param>
    /// <returns>Lista de recompensas do filho</returns>
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

    /// <summary>
    /// Obtem os detalhes de uma recompensa específica por seu ID.
    /// </summary>
    /// <param name="id">ID da recompensa</param>
    /// <returns>Detalhes da recompensa</returns>
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

    /// <summary>
    /// Criar uma nova recompensa para um filho específico. O DTO deve conter o ID do filho, a descrição da recompensa e os pontos necessários para resgatá-la.
    /// </summary>
    /// <param name="dto">Dados da recompensa</param>
    /// <returns>Resultado da operação</returns>
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
    /// <summary>
    /// Atualizar os detalhes de uma recompensa existente, como a descrição ou os pontos necessários.
    /// </summary>
    /// <param name="id">ID da recompensa</param>
    /// <param name="dto">Novos dados da recompensa</param>
    /// <returns>Resultado da operação</returns>
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

    /// <summary>
    /// Deletar uma recompensa existente, removendo-a do sistema. Essa ação deve ser confirmada para evitar exclusões acidentais.
    /// </summary>
    /// <param name="id">ID da recompensa</param>
    /// <returns>Resultado da operação</returns>
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

    /// <summary>
    /// Resgatar uma recompensa para um filho específico.
    /// </summary>
    /// <param name="filhoId">ID do filho</param>
    /// <param name="recompensaId">ID da recompensa</param>
    /// <returns>Resultado da operação</returns>
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
    /// <summary>
    /// Retornar uma lista de todas as recompensas que um filho específico resgatou, incluindo detalhes como a data do resgate e a descrição da recompensa.
    /// </summary>
    /// <param name="filhoId">ID do filho</param>
    /// <returns>Lista de recompensas resgatadas</returns>
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