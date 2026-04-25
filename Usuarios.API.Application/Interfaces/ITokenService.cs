using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Application.Interfaces;

public interface ITokenService
{
    string GerarToken(Usuario usuario);
}
