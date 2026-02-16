using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Entities;
using Usuarios.API.Domain.Repositories;
using Usuarios.API.Infrastructure.Data;

namespace Usuarios.API.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly UsuarioContext _context;

    public UsuarioRepository(UsuarioContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObterPorIdAsync(int id)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Usuario>> ObterTodosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(int id)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}