using Microsoft.AspNetCore.Mvc;
using Usuarios.API.Application.DTOs;
using Usuarios.API.Application.Interfaces;

namespace Usuarios.API.Controllers.v1;

[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuarioController(IUsuarioService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("api/v1/[controller]/ObterTodos")]
    public async Task<IActionResult> ObterTodos()
    {
        var usuarios = await _service.ObterTodosAsync();
        return Ok(usuarios);
    }

    [HttpGet]
    [Route("api/v1/[controller]/ObterPorId/{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var usuario = await _service.ObterPorIdAsync(id);

        if (usuario == null)
        {
            return NotFound(new { mensagem = "Usuário não encontrado" });
        }

        return Ok(usuario);
    }

    [HttpPost]
    [Route("api/v1/[controller]/AdicionarUsuario")]
    public async Task<IActionResult> Criar([FromBody] UsuarioDto dto)
    {
        var usuario = await _service.CriarAsync(dto);
        return Ok();
    }

    [HttpPut]
    [Route("api/v1/[controller]/AtualizarUsuario/{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] UsuarioDto dto)
    {
        await _service.AtualizarAsync(id, dto);
        return Ok("UsuarioAtualizado");
    }

    [HttpDelete]
    [Route("api/v1/[controller]/RemoverUsuario/{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        await _service.RemoverAsync(id);
        return Ok("Usuario removido");
    }
}
