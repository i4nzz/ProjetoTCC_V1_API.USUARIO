using GestaoTarefas.Domain.Entities;

namespace GestaoTarefas.Application.Interfaces;

public interface ITokenService
{
    string GerarToken(Usuario usuario);
}
