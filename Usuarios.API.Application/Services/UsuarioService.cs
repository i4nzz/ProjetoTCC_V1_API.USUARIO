using Usuarios.API.Application.DTOs;
using Usuarios.API.Application.Interfaces;
using Usuarios.API.Domain.Repositories;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Usuario>> ObterTodosAsync()
    {
        //return usuarios.Select(u => MapToDto(u));
        return await _repository.ObterTodosAsync();
    }

    public async Task<Usuario?> ObterPorIdAsync(int id)
    {
        return await _repository.ObterPorIdAsync(id);
    }

    public async Task<UsuarioDto> CriarAsync(UsuarioDto dto)
    {
        var usuario = new Usuario(dto.Nome, dto.Email, dto.Telefone);
        await _repository.AdicionarAsync(usuario);

        return MapToDto(usuario);
    }

    public async Task AtualizarAsync(int id, UsuarioDto dto)
    {
        var usuario = await _repository.ObterPorIdAsync(id);

        if (usuario == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado");
        }

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;
        usuario.Telefone = dto.Telefone;

        await _repository.AtualizarAsync(usuario);
    }

    public async Task RemoverAsync(int id)
    {
        var usuario = await _repository.ObterPorIdAsync(id);

        if (usuario == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado");
        }

        await _repository.RemoverAsync(id);
    }

    private static UsuarioDto MapToDto(Usuario usuario)
    {
        return new UsuarioDto
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
            Telefone = usuario.Telefone
        };
    }
}