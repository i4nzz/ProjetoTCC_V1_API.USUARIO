using System.Security.Claims;
using GestaoTarefas.API.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GestaoTarefas.API.Application.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UsuarioId
    {
        get
        {
            var valor = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(valor, out var id) ? id : throw new UnauthorizedAccessException("Usuário não autenticado.");
        }
    }

    public string Perfil => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role) ?? throw new UnauthorizedAccessException("Usuário não autenticado.");
}
