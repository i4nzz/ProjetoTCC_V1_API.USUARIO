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

    [HttpGet("tarefa/{tarefaId:int}")]
    public async Task<IActionResult> ObterPorTarefa(int tarefaId)
    {
        var resultado = await _comprovacaoTarefaService.ObterPorTarefaAsync(tarefaId);

        if (!resultado.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, resultado.Mensagem);
        }

        return StatusCode((int)resultado.StatusCode, resultado);
    }

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

    [HttpPatch("validar/{id:int}")]
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