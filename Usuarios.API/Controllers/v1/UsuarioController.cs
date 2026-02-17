using Microsoft.AspNetCore.Mvc;
using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs;
using Usuarios.API.Application.Interfaces;
using Usuarios.API.Application.Mapping;

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

        return Ok(new ResponseApi<IEnumerable<UsuarioDto>>(usuarios.ToDtoList(), "Usuários obtidos com sucesso"));
    }
    [HttpGet]
    [Route("api/v1/[controller]/ObterPorId/{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var usuario = await _service.ObterPorIdAsync(id);

        if (usuario == null)
        {
            return NotFound(new ResponseApi<UsuarioDto>(new List<string> { "Usuário não encontrado" }));
        }

        return Ok(new ResponseApi<UsuarioDto>(usuario.ToDto(),"Usuário obtido com sucesso"));
    }


    [HttpPost]
    [Route("api/v1/[controller]/AdicionarUsuario")]
    public async Task<IActionResult> Criar([FromBody] UsuarioDto dto)
    {
        var usuario = await _service.CriarAsync(dto);

        return Ok(new ResponseApi<UsuarioDto>(usuario, "Usuário criado com sucesso"));
    }

    [HttpPut]
    [Route("api/v1/[controller]/AtualizarUsuario/{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] UsuarioDto dto)
    {
        await _service.AtualizarAsync(id, dto);
        return Ok(new ResponseApi<string>("Usuário atualizado com sucesso"));
    }

    [HttpDelete]
    [Route("api/v1/[controller]/RemoverUsuario/{id}")] 
    public async Task<IActionResult> Remover(int id)
    {
        await _service.RemoverAsync(id);
        return Ok(new ResponseApi<string>("Usuário removido com sucesso"));
    }
}
