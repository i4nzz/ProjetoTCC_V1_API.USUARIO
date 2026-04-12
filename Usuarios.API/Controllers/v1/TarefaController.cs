using Microsoft.AspNetCore.Mvc;
using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Tarefa;
using Usuarios.API.Application.Interfaces;

namespace Usuarios.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
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

        if (tarefas == null || !tarefas.Any())
            return NotFound(ResponseApi<RetornoTarefaDto>.Erro("Tarefa não encontrada"));

        return Ok(new ResponseApi<IEnumerable<RetornoTarefaDto>>(tarefas, "Tarefas obtidas com sucesso"));
    }

    [HttpGet("filho/{filhoId:int}")]
    public async Task<IActionResult> ObterPorFilho(int filhoId)
    {
        var tarefas = await _tarefaService.ObterPorFilhoAsync(filhoId);

        if (!tarefas.Any())
            return NotFound(ResponseApi<RetornoTarefaDto>.Erro("Tarefa não encontrada"));

        return Ok(new ResponseApi<IEnumerable<RetornoTarefaDto>>(tarefas));
    }

    [HttpGet("{tarefaId:int}")]
    public async Task<IActionResult> ObterPorId(int tarefaId)
    {
        var tarefa = await _tarefaService.ObterPorIdAsync(tarefaId);

        if (tarefa == null)
            return NotFound(ResponseApi<RetornoTarefaDto>.Erro("Tarefa não encontrada"));

        return Ok(new ResponseApi<RetornoTarefaDto>(tarefa));
    }

    [HttpPost("CriarTarefa")]
    public async Task<IActionResult> Criar([FromBody] CriarTarefaDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResponseApi<string>("Dados inválidos"));

        var tarefa = await _tarefaService.CriarAsync(dto);

        return CreatedAtAction(nameof(ObterPorId), new { tarefaId = tarefa.TarefaId }, new ResponseApi<RetornoTarefaDto>(tarefa, "Tarefa criada com sucesso")
        );
    }

    [HttpPut("{tarefaId:int}")]
    public async Task<IActionResult> Atualizar(int tarefaId, [FromBody] CriarTarefaDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResponseApi<string>("Dados inválidos"));

        var existe = await _tarefaService.ObterPorIdAsync(tarefaId);

        if (existe == null)
            return NotFound(ResponseApi<RetornoTarefaDto>.Erro("Tarefa não encontrada"));

        await _tarefaService.AtualizarAsync(tarefaId, dto);

        return Ok(ResponseApi<string>.Ok("Tarefa atualizada com sucesso"));
    }

    [HttpDelete("{tarefaId:int}")]
    public async Task<IActionResult> Remover(int tarefaId)
    {
        var existe = await _tarefaService.ObterPorIdAsync(tarefaId);

        if (existe == null)
            return NotFound(new ResponseApi<string>("Tarefa não encontrada"));

        await _tarefaService.RemoverAsync(tarefaId);

        return Ok(new ResponseApi<string>("Tarefa removida com sucesso"));
    }
}