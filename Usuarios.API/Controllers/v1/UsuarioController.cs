using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GestaoTarefas.Application.DTOs.Usuario;
using GestaoTarefas.Application.Interfaces;

namespace GestaoTarefas.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }
    /// <summary>
    /// Obter todos os usuários cadastrados.
    /// </summary>
    /// <returns>Lista de usuários</returns>
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
    /// <summary>
    /// Obter um usuário específico pelo seu ID.
    /// </summary>
    /// <param name="id">ID do usuário</param>
    /// <returns>Usuário correspondente ao ID fornecido</returns>
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
    /// <summary>
    /// Criar um novo usuario. Usuario padrão é o Pai.
    /// </summary>
    /// <param name="dto">Dados para criar o usuário</param>
    /// <returns>Resultado da operação</returns>
    [HttpPost]
    [Route("AdicionarUsuarioPai")]
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
    /// <summary>
    /// Atualizar as informações de um usuário existente, como nome, email, senha e perfil.
    /// </summary>
    /// <param name="id">ID do usuário</param>
    /// <param name="dto">Dados para atualizar o usuário</param>
    /// <returns>Resultado da operação</returns>
    [HttpPut]
    [Route("AtualizarUsuario/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarUsuarioDto dto)
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
    /// <summary>
    /// Remover um usuario do sistema. Se o usuário for um pai, todos os filhos associados a ele também serão removidos.
    /// </summary>
    /// <param name="id">ID do usuário</param>
    /// <returns>Resultado da operação</returns>
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
    /// <summary>
    /// Criar um usuario tipo Filho.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
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