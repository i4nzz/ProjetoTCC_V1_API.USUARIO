using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> ObterTodos()
    {
        var usuarios = await _usuarioService.ObterTodosAsync();

        if (!usuarios.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.NotFound, usuarios);
        }

        return StatusCode((int)HttpStatusCode.OK, usuarios.ObjetoRetorno);
    }

    [HttpGet]
    [Route("ObterPorId/{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var usuario = await _usuarioService.ObterPorIdAsync(id);

        if (!usuario.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.NotFound, usuario);
        }

        return StatusCode((int)HttpStatusCode.OK, usuario.ObjetoRetorno);
    }

    [HttpPost]
    [Route("AdicionarUsuario")]
    [AllowAnonymous]
    public async Task<IActionResult> Criar([FromBody] CriarUsuarioDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var usuario = await _usuarioService.CriarUsuarioAsync(dto);

        if (!usuario.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, usuario);
        }

        return StatusCode((int)HttpStatusCode.Created, usuario.ObjetoRetorno);
    }

    [HttpPut]
    [Route("AtualizarUsuario/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Atualizar(int id, [FromBody] CriarUsuarioDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var usuario = await _usuarioService.AtualizarAsync(id, dto);

        if (!usuario.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, usuario);
        }

        return StatusCode((int)HttpStatusCode.OK, usuario.ObjetoRetorno);
    }

    [HttpDelete]
    [Route("RemoverUsuario/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Remover(int id)
    {
        var usuario = await _usuarioService.RemoverAsync(id);

        if (!usuario.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, usuario);
        }

        return StatusCode((int)HttpStatusCode.OK, usuario.ObjetoRetorno);
    }

    [HttpPost]
    [Route("AdicionarFilho")]
    [Authorize(Roles = "Pai")]
    public async Task<IActionResult> CriarFilho([FromBody] CriarFilhoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var resultado = await _usuarioService.CriarFilhoAsync(dto);

        if (!resultado.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, resultado);
        }

        return StatusCode((int)HttpStatusCode.Created, resultado.ObjetoRetorno);
    }
}