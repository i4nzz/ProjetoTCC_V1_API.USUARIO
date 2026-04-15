using System.Net;
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
    public async Task<RespostaMetodos<IEnumerable<RetornoTarefaDto>>> ObterTodas()
    {
        var tarefas = await _tarefaService.ObterTodasAsync();

        if (tarefas == null || !tarefas.Any())
        {
            return new RespostaMetodos<IEnumerable<RetornoTarefaDto>>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                ObjetoRetorno = null,
                Mensagem = "Nenhuma tarefa encontrada"
            };
        }

        return new RespostaMetodos<IEnumerable<RetornoTarefaDto>>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = tarefas,
            Mensagem = "Tarefas obtidas com sucesso"
        };
    }

    [HttpGet("filho/{filhoId:int}")]
    public async Task<RespostaMetodos<IEnumerable<RetornoTarefaDto>>> ObterPorFilho(int filhoId)
    {
        var tarefas = await _tarefaService.ObterPorFilhoAsync(filhoId);

        if (tarefas == null || !tarefas.Any())
        {
            return new RespostaMetodos<IEnumerable<RetornoTarefaDto>>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                ObjetoRetorno = null,
                Mensagem = "Nenhuma tarefa encontrada"
            };
        }

        return new RespostaMetodos<IEnumerable<RetornoTarefaDto>>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = tarefas,
            Mensagem = "Tarefas obtidas com sucesso"
        };
    }

    [HttpGet("{tarefaId:int}")]
    public async Task<RespostaMetodos<RetornoTarefaDto>> ObterPorId(int tarefaId)
    {
        var tarefa = await _tarefaService.ObterPorIdAsync(tarefaId);

        if (tarefa == null)
        {
            return new RespostaMetodos<RetornoTarefaDto>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                ObjetoRetorno = null,
                Mensagem = "Tarefa não encontrada"
            };
        }

        return new RespostaMetodos<RetornoTarefaDto>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = tarefa,
            Mensagem = "Tarefa obtida com sucesso"
        };
    }

    [HttpPost("CriarTarefa")]
    public async Task<RespostaMetodos<RetornoTarefaDto>> Criar([FromBody] CriarTarefaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return new RespostaMetodos<RetornoTarefaDto>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.BadRequest,
                ObjetoRetorno = null,
                Mensagem = "Dados inválidos"
            };
        }

        var tarefa = await _tarefaService.CriarAsync(dto);

        return new RespostaMetodos<RetornoTarefaDto>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.Created,
            ObjetoRetorno = tarefa,
            Mensagem = "Tarefa criada com sucesso"
        };
    }

    [HttpPut("{tarefaId:int}")]
    public async Task<RespostaMetodos<string>> Atualizar(int tarefaId, [FromBody] CriarTarefaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return new RespostaMetodos<string>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.BadRequest,
                ObjetoRetorno = null,
                Mensagem = "Dados inválidos"
            };
        }

        var existe = await _tarefaService.ObterPorIdAsync(tarefaId);

        if (existe == null)
        {
            return new RespostaMetodos<string>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                ObjetoRetorno = null,
                Mensagem = "Tarefa não encontrada"
            };
        }

        await _tarefaService.AtualizarAsync(tarefaId, dto);

        return new RespostaMetodos<string>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = "OK",
            Mensagem = "Tarefa atualizada com sucesso"
        };
    }

    [HttpDelete("{tarefaId:int}")]
    public async Task<RespostaMetodos<string>> Remover(int tarefaId)
    {
        var existe = await _tarefaService.ObterPorIdAsync(tarefaId);

        if (existe == null)
        {
            return new RespostaMetodos<string>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                ObjetoRetorno = null,
                Mensagem = "Tarefa não encontrada"
            };
        }

        await _tarefaService.RemoverAsync(tarefaId);

        return new RespostaMetodos<string>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = "OK",
            Mensagem = "Tarefa removida com sucesso"
        };
    }
}