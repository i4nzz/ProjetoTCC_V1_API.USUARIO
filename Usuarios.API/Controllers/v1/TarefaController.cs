using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Usuarios.API.Application.DTOs.Tarefa;
using Usuarios.API.Application.Interfaces;

namespace Usuarios.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class TarefaController : ControllerBase
{
    private readonly ITarefaService _tarefaService;

    public TarefaController(ITarefaService tarefaService)
    {
        _tarefaService = tarefaService;
    }

    [HttpGet("ObterTodas")]
    public async Task<IActionResult> ObterTodas()
    {
        var tarefas = await _tarefaService.ObterTodasAsync();

        if (!tarefas.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, tarefas.Mensagem);
        }

        return StatusCode((int)HttpStatusCode.OK, tarefas.ObjetoRetorno);
    }

    [HttpGet("filho/{filhoId:int}")]
    [Authorize(Roles = "Pai")]
    public async Task<IActionResult> ObterPorFilho(int filhoId)
    {
        var tarefas = await _tarefaService.ObterPorFilhoAsync(filhoId);

        if (!tarefas.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.NotFound, tarefas.Mensagem);
        }

        return StatusCode((int)HttpStatusCode.OK, tarefas.ObjetoRetorno);
    }

    [HttpGet("{tarefaId:int}")]
    [Authorize(Roles = "Pai")]
    public async Task<IActionResult> ObterPorId(int tarefaId)
    {
        var tarefa = await _tarefaService.ObterPorIdAsync(tarefaId);

        if (!tarefa.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.NotFound, tarefa.Mensagem);
        }

        return StatusCode((int)HttpStatusCode.OK, tarefa.ObjetoRetorno);
    }

    [HttpPost("CriarTarefa")]
    [Authorize(Roles = "Pai")]
    public async Task<IActionResult> Criar([FromBody] CriarTarefaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, "Dados inválidos");
        }

        var tarefa = await _tarefaService.CriarAsync(dto);

        if (!tarefa.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, tarefa.Mensagem);
        }

        return StatusCode((int)HttpStatusCode.Created, tarefa.ObjetoRetorno);

    }

    [HttpPut("{tarefaId:int}")]
    [Authorize(Roles = "Pai")]
    public async Task<IActionResult> Atualizar(int tarefaId, [FromBody] CriarTarefaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, "Dados inválidos");
        }

        var existe = await _tarefaService.ObterPorIdAsync(tarefaId);

        if (!existe.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.NotFound, existe.Mensagem);
        }

        var resultado = await _tarefaService.AtualizarAsync(tarefaId, dto);

        return StatusCode((int)HttpStatusCode.OK, resultado.Mensagem);
    }

    [HttpDelete("{tarefaId:int}")]
    [Authorize(Roles = "Pai")]
    public async Task<IActionResult> Remover(int tarefaId)
    {
        var existe = await _tarefaService.ObterPorIdAsync(tarefaId);

        if (!existe.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.NotFound, existe.Mensagem);
        }

        var removido = await _tarefaService.RemoverAsync(tarefaId);

        return StatusCode((int)HttpStatusCode.OK, removido.Mensagem);
    }
}