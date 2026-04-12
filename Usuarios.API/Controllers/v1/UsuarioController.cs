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
    public async Task<IActionResult> ObterTodos()
    {
        var usuarios = await _usuarioService.ObterTodosAsync();
        return Ok(new ResponseApi<IEnumerable<RetornoUsuarioDto>>(usuarios, "Usuários obtidos com sucesso"));
    }

    [HttpGet]
    [Route("ObterPorId/{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var usuario = await _usuarioService.ObterPorIdAsync(id);
        if (usuario == null)
            return NotFound(ResponseApi<RetornoUsuarioDto>.Erro("Usuário não encontrado"));

        return Ok(new ResponseApi<RetornoUsuarioDto>(usuario, "Usuário obtido com sucesso"));
    }

    [HttpPost]
    [Route("/AdicionarUsuario")]
    public async Task<IActionResult> Criar([FromBody] CriarUsuarioDto dto)
    {
        var usuario = await _usuarioService.CriarAsync(dto);
        return Ok(new ResponseApi<RetornoUsuarioDto>(usuario, "Usuário criado com sucesso"));
    }

    [HttpPut]
    [Route("AtualizarUsuario/{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] CriarUsuarioDto dto)
    {
        await _usuarioService.AtualizarAsync(id, dto);
        return Ok(new ResponseApi<string>("Usuário atualizado com sucesso"));
    }

    [HttpDelete]
    [Route("RemoverUsuario/{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        await _usuarioService.RemoverAsync(id);
        return Ok(new ResponseApi<string>("Usuário removido com sucesso"));
    }
}