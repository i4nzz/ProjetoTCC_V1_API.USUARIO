using Microsoft.AspNetCore.Mvc;
using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Usuario;
using Usuarios.API.Application.Interfaces;

namespace Usuarios.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuarioController(IUsuarioService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("ObterTodos")]
    public async Task<IActionResult> ObterTodos()
    {
        var usuarios = await _service.ObterTodosAsync();
        return Ok(new ResponseApi<IEnumerable<RetornoUsuarioDto>>(usuarios, "Usuários obtidos com sucesso"));
    }

    [HttpGet]
    [Route("ObterPorId/{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var usuario = await _service.ObterPorIdAsync(id);
        if (usuario == null)
            return NotFound(new ResponseApi<RetornoUsuarioDto>(new List<string> { "Usuário não encontrado" }));

        return Ok(new ResponseApi<RetornoUsuarioDto>(usuario, "Usuário obtido com sucesso"));
    }

    [HttpPost]
    [Route("/AdicionarUsuario")]
    public async Task<IActionResult> Criar([FromBody] CriarUsuarioDto dto)
    {
        var usuario = await _service.CriarAsync(dto);
        return Ok(new ResponseApi<RetornoUsuarioDto>(usuario, "Usuário criado com sucesso"));
    }

    [HttpPut]
    [Route("AtualizarUsuario/{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] CriarUsuarioDto dto)
    {
        await _service.AtualizarAsync(id, dto);
        return Ok(new ResponseApi<string>("Usuário atualizado com sucesso"));
    }

    [HttpDelete]
    [Route("RemoverUsuario/{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        await _service.RemoverAsync(id);
        return Ok(new ResponseApi<string>("Usuário removido com sucesso"));
    }
}