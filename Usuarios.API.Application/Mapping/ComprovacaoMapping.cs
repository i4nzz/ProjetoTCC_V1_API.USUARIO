using Usuarios.API.Application.DTOs.Recompensa;
using Usuarios.API.Domain.Entities;

namespace Usuarios.API.Application.Mapping;

public static class ComprovacaoMapping
{
    public static RetornoComprovacaoDto ToDto(this ComprovacaoTarefa comprovacao)
    {
        if (comprovacao is null)
            throw new ArgumentNullException(nameof(comprovacao));

        return new RetornoComprovacaoDto
        {
            Id = comprovacao.Id,
            TarefaId = comprovacao.TarefaId,
            TituloTarefa = comprovacao.Tarefa?.Titulo ?? string.Empty,
            UrlFoto = comprovacao.UrlFoto,
            Validada = comprovacao.Validada,
            DataEnvio = comprovacao.DataEnvio,
            DataValidacao = comprovacao.DataValidacao
        };
    }

    public static IEnumerable<RetornoComprovacaoDto> ToDtoList(this IEnumerable<ComprovacaoTarefa> comprovacoes)
    {
        if (comprovacoes is null)
            return Enumerable.Empty<RetornoComprovacaoDto>();

        return comprovacoes.Select(c => c.ToDto());
    }
}
