using Usuarios.API.Application.DTOs.Usuario;
using Usuarios.API.Application.Interfaces;
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
        return usuarios.Select(MapToDto);
    }

    public async Task<RetornoUsuarioDto?> ObterPorIdAsync(int id)
    {
        var usuario = await _repository.ObterPorIdAsync(id);
        return usuario == null ? null : MapToDto(usuario);
    }

    public async Task<RetornoUsuarioDto> CriarAsync(CriarUsuarioDto dto)
    {
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
        var usuario = new Usuario(dto.Nome, dto.Email, senhaHash, dto.Perfil);
        await _repository.AdicionarAsync(usuario);
        return MapToDto(usuario);
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

    private static RetornoUsuarioDto MapToDto(Usuario usuario)
    {
        return new RetornoUsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Perfil = usuario.Perfil,
            Ativo = usuario.Ativo
        };
    }
}