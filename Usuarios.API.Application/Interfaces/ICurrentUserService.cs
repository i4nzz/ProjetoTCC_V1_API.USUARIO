namespace GestaoTarefas.API.Application.Interfaces;

public interface ICurrentUserService
{
    int UsuarioId { get; }
    string Perfil { get; }
}
