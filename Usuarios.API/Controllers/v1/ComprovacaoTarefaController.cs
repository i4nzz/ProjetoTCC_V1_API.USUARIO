using System.Net;
using Microsoft.AspNetCore.Mvc;
using Usuarios.API.Application.Common.Responses;
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
    public async Task<RespostaMetodos<IEnumerable<RetornoComprovacaoDto>>> ObterPorTarefa(int tarefaId)
    {
        var comprovacoes = await _comprovacaoTarefaService.ObterPorTarefaAsync(tarefaId);

        if (comprovacoes == null || !comprovacoes.Any())
        {
            return new RespostaMetodos<IEnumerable<RetornoComprovacaoDto>>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                ObjetoRetorno = null,
                Mensagem = "Nenhuma comprovação encontrada"
            };
        }

        return new RespostaMetodos<IEnumerable<RetornoComprovacaoDto>>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = comprovacoes,
            Mensagem = "Comprovações obtidas com sucesso"
        };
    }

    [HttpGet("{id:int}")]
    public async Task<RespostaMetodos<RetornoComprovacaoDto>> ObterPorId(int id)
    {
        var comprovacao = await _comprovacaoTarefaService.ObterPorIdAsync(id);

        if (comprovacao == null)
        {
            return new RespostaMetodos<RetornoComprovacaoDto>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                ObjetoRetorno = null,
                Mensagem = "Comprovação não encontrada"
            };
        }


        return new RespostaMetodos<RetornoComprovacaoDto>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = comprovacao,
            Mensagem = "Comprovação obtida com sucesso"
        };
    }

    [HttpPost("Enviar")]
    public async Task<RespostaMetodos<RetornoComprovacaoDto>> Enviar([FromBody] CriarComprovacaoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return new RespostaMetodos<RetornoComprovacaoDto>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.BadRequest,
                ObjetoRetorno = null,
                Mensagem = "Dados inválidos"
            };
        }


        var comprovacao = await _comprovacaoTarefaService.EnviarAsync(dto);

        return new RespostaMetodos<RetornoComprovacaoDto>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.Created,
            ObjetoRetorno = comprovacao,
            Mensagem = "Comprovação enviada com sucesso"
        };
    }

    [HttpPatch("Validar/{id:int}")]
    public async Task<RespostaMetodos<RetornoComprovacaoDto>> Validar(int id)
    {
        var comprovacao = await _comprovacaoTarefaService.ObterPorIdAsync(id);

        if (comprovacao == null)
        {
            return new RespostaMetodos<RetornoComprovacaoDto>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                ObjetoRetorno = null,
                Mensagem = "Comprovação não encontrada"
            };
        }


        var resultado = await _comprovacaoTarefaService.ValidarAsync(id);

        return new RespostaMetodos<RetornoComprovacaoDto>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = resultado,
            Mensagem = "Comprovação validada com sucesso"
        };
    }
}