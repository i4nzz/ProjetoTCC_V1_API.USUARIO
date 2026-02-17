using Usuarios.API.Application.DTOs;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Application.Mapping
{
    public static class UsuarioMapping
    {
        public static UsuarioDto ToDto(this Usuario usuario)
        {
            return new UsuarioDto
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                Ativo = usuario.Ativo
            };
        }

        public static IEnumerable<UsuarioDto> ToDtoList(this IEnumerable<Usuario> usuarios)
        {
            return usuarios.Select(u => u.ToDto());
        }
    }
}
