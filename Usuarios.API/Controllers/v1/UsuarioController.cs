using System.Net;
using Microsoft.AspNetCore.Mvc;
using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Usuario;
using Usuarios.API.Application.Interfaces;

namespace Usuarios.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    [Route("ObterTodos")]
    public async Task<RespostaMetodos<IEnumerable<RetornoUsuarioDto>>> ObterTodos()
    {
        var usuarios = await _usuarioService.ObterTodosAsync();

        if (usuarios == null || !usuarios.Any())
        {
            return new RespostaMetodos<IEnumerable<RetornoUsuarioDto>>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NoContent,
                ObjetoRetorno = null,
                Mensagem = "Nenhum usuário encontrado"
            };
        }

        return new RespostaMetodos<IEnumerable<RetornoUsuarioDto>>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = usuarios,
            Mensagem = "Usuários obtidos com sucesso"
        };
    }

    [HttpGet]
    [Route("ObterPorId/{id}")]
    public async Task<RespostaMetodos<RetornoUsuarioDto>> ObterPorId(int id)
    {
        var usuario = await _usuarioService.ObterPorIdAsync(id);

        if (usuario == null)
        {
            return new RespostaMetodos<RetornoUsuarioDto>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                ObjetoRetorno = null,
                Mensagem = "Usuário não encontrado"
            };
        }

        return new RespostaMetodos<RetornoUsuarioDto>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = usuario,
            Mensagem = "Usuário obtido com sucesso"
        };
    }

    [HttpPost]
    [Route("AdicionarUsuario")]
    public async Task<RespostaMetodos<RetornoUsuarioDto>> Criar([FromBody] CriarUsuarioDto dto)
    {
        var usuario = await _usuarioService.CriarAsync(dto);

        return new RespostaMetodos<RetornoUsuarioDto>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.Created,
            ObjetoRetorno = usuario,
            Mensagem = "Usuário criado com sucesso"
        };
    }

    [HttpPut]
    [Route("AtualizarUsuario/{id}")]
    public async Task<RespostaMetodos<string>> Atualizar(int id, [FromBody] CriarUsuarioDto dto)
    {
        await _usuarioService.AtualizarAsync(id, dto);

        return new RespostaMetodos<string>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = "OK",
            Mensagem = "Usuário atualizado com sucesso"
        };
    }

    [HttpDelete]
    [Route("RemoverUsuario/{id}")]
    public async Task<RespostaMetodos<string>> Remover(int id)
    {
        await _usuarioService.RemoverAsync(id);

        return new RespostaMetodos<string>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = "OK",
            Mensagem = "Usuário removido com sucesso"
        };
    }
}