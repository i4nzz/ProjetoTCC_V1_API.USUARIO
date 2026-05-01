using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Usuarios.API.Application.DTOs.Recompensa;
using Usuarios.API.Application.Interfaces;

namespace Usuarios.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ComprovacaoTarefaController : ControllerBase
{
    private readonly IComprovacaoService _comprovacaoTarefaService;

    public ComprovacaoTarefaController(IComprovacaoService comprovacaoTarefaService)
    {
        _comprovacaoTarefaService = comprovacaoTarefaService;
    }

    /// <summary>
    /// Obter comprovações de uma tarefa específica
    /// </summary>
    /// <param name="tarefaId">ID da tarefa</param>
    /// <returns>Lista de comprovações da tarefa</returns>
    [HttpGet("tarefa/{tarefaId:int}")]
    public async Task<IActionResult> ObterPorTarefa(int tarefaId)
    {
        var resultado = await _comprovacaoTarefaService.ObterPorTarefaAsync(tarefaId);

        if (!resultado.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, resultado.Mensagem);
        }

        return StatusCode((int)HttpStatusCode.OK, resultado);
    }
    /// <summary>
    /// Obter comprovações de uma tarefa específica pelo seu ID
    /// </summary>
    /// <param name="id">ID da comprovação</param>
    /// <returns>Comprovação da tarefa</returns>

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var resultado = await _comprovacaoTarefaService.ObterPorIdAsync(id);

        if (!resultado.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, resultado.Mensagem);
        }

        return StatusCode((int)HttpStatusCode.OK, resultado.ObjetoRetorno);
    }

    /// <summary>
    /// Enviar comprovação de tarefa para validação
    /// </summary>
    /// <param name="dto">Dados da comprovação</param>
    /// <returns>Resultado da operação</returns>
    [HttpPost("enviar")]
    [Authorize(Roles = "Filho")]
    public async Task<IActionResult> Enviar([FromBody] CriarComprovacaoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        var resultado = await _comprovacaoTarefaService.EnviarAsync(dto);

        if (!resultado.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, resultado.Mensagem);
        }

        return StatusCode((int)(resultado.StatusCode == 0 ? HttpStatusCode.Created : resultado.StatusCode), resultado);
    }
    /// <summary>
    /// Validar comprovação de tarefa (aprovando ou rejeitando)
    /// </summary>
    /// <param name="id">ID da comprovação</param>
    /// <returns>Resultado da operação</returns>
    [HttpPost("validar/{id:int}")]
    [Authorize(Roles = "Pai")]
    public async Task<IActionResult> Validar(int id)
    {
        var resultado = await _comprovacaoTarefaService.ValidarAsync(id);

        if (!resultado.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, resultado.Mensagem);
        }

        return StatusCode((int)resultado.StatusCode, resultado.ObjetoRetorno);
    }
}