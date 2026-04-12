using Usuarios.API.Application.DTOs.Usuario;
using Usuarios.API.Application.Interfaces;
using Usuarios.API.Application.Mapping;
using Usuarios.API.Domain.Entities;
using Usuarios.API.Domain.Interfaces;

namespace Usuarios.API.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RetornoUsuarioDto>> ObterTodosAsync()
    {
        var usuarios = await _repository.ObterTodosAsync();
        return usuarios.ToDtoList();
    }

    public async Task<RetornoUsuarioDto?> ObterPorIdAsync(int id)
    {
        var usuario = await _repository.ObterPorIdAsync(id);
        return usuario == null ? null : usuario.ToDto();
    }

    public async Task<RetornoUsuarioDto> CriarAsync(CriarUsuarioDto dto)
    {
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
        var usuario = new Usuario(dto.Nome, dto.Email, senhaHash, dto.Perfil);
        await _repository.AdicionarAsync(usuario);
        return usuario.ToDto();
    }

    public async Task AtualizarAsync(int id, CriarUsuarioDto dto)
    {
        var usuario = await _repository.ObterPorIdAsync(id);
        if (usuario == null)
            throw new KeyNotFoundException("Usuário não encontrado");

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;
        await _repository.AtualizarAsync(usuario);
    }

    public async Task RemoverAsync(int id)
    {
        var usuario = await _repository.ObterPorIdAsync(id);
        if (usuario == null)
            throw new KeyNotFoundException("Usuário não encontrado");

        await _repository.RemoverAsync(id);
    }
}