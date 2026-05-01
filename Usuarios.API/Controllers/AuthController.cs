using System.Net;
using Microsoft.AspNetCore.Mvc;
using GestaoTarefas.Application.DTOs.Login;
using GestaoTarefas.Application.Interfaces;

namespace GestaoTarefas.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public AuthController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }
    /// <summary>
    /// Retorna o token JWT.
    /// </summary>
    /// <param name="dto">Dados de login</param>
    /// <returns>Token JWT</returns>
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var resultado = await _usuarioService.LoginAsync(dto);

        if (!resultado.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.Unauthorized, resultado);
        }

        return StatusCode((int)HttpStatusCode.OK, resultado.ObjetoRetorno);
    }
}