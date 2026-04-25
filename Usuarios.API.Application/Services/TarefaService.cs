using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Tarefa;
using Usuarios.API.Application.Interfaces;
using Usuarios.API.Application.Mapping;
using Usuarios.API.Domain.Entities;
using Usuarios.API.Domain.Interfaces;

namespace Usuarios.API.Application.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public TarefaService(
        ITarefaRepository tarefaRepository
        , IUsuarioRepository usuarioRepository
        )
    {
        _tarefaRepository = tarefaRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<RespostaMetodos<IEnumerable<RetornoTarefaDto>>> ObterTodasAsync()
    {
        var tarefas = await _tarefaRepository.ObterTodasAsync();

        if (tarefas == null || !tarefas.Any())
        {
            return new RespostaMetodos<IEnumerable<RetornoTarefaDto>>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Nenhuma tarefa encontrada"
            };
        }

        var retornoTarefas = tarefas.ToDtoList();

        return new RespostaMetodos<IEnumerable<RetornoTarefaDto>>
        {
            Sucesso = true,
            ObjetoRetorno = retornoTarefas,
            Mensagem = "Tarefas obtidas com sucesso"
        };
    }

    public async Task<RespostaMetodos<IEnumerable<RetornoTarefaDto>>> ObterPorFilhoAsync(int filhoId)
    {
        var tarefas = await _tarefaRepository.ObterPorFilhoAsync(filhoId);

        if (tarefas == null || !tarefas.Any())
        {
            return new RespostaMetodos<IEnumerable<RetornoTarefaDto>>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Nenhuma tarefa encontrada para o filho especificado"
            };
        }

        var retornoTarefas = tarefas.ToDtoList();

        return new RespostaMetodos<IEnumerable<RetornoTarefaDto>>
        {
            Sucesso = true,
            ObjetoRetorno = retornoTarefas,
            Mensagem = "Tarefas obtidas com sucesso"
        };
    }

    public async Task<RespostaMetodos<RetornoTarefaDto?>> ObterPorIdAsync(int tarefaId)
    {
        var tarefa = await _tarefaRepository.ObterPorIdAsync(tarefaId);

        if (tarefa == null)
        {
            return new RespostaMetodos<RetornoTarefaDto?>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Tarefa não encontrada"
            };
        }

        var retornoTarefa = tarefa.ToDto();

        return new RespostaMetodos<RetornoTarefaDto?>
        {
            Sucesso = true,
            ObjetoRetorno = retornoTarefa,
            Mensagem = "Tarefa obtida com sucesso"
        };
    }

    public async Task<RespostaMetodos<RetornoTarefaDto>> CriarAsync(CriarTarefaDto dto)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(dto.FilhoId);

        if (usuario == null)
        {
            return new RespostaMetodos<RetornoTarefaDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Filho não encontrado"
            };
        }

        if (dto.Prazo <= DateTime.UtcNow)
        {
            return new RespostaMetodos<RetornoTarefaDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "O prazo da tarefa deve ser uma data futura"
            };
        }

        if (dto.Pontos <= 0)
        {
            return new RespostaMetodos<RetornoTarefaDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Os pontos da tarefa devem ser maiores que zero"
            };
        }

        var tarefa = new Tarefa
        {
            FilhoId = dto.FilhoId,
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Pontos = dto.Pontos,
            Prazo = dto.Prazo
        };

        var retornoTarefa = tarefa.ToDto();

        await _tarefaRepository.AdicionarAsync(tarefa);

        return new RespostaMetodos<RetornoTarefaDto>
        {
            Sucesso = true,
            ObjetoRetorno = retornoTarefa,
            Mensagem = "Tarefa criada com sucesso"
        };
    }

    public async Task<RespostaMetodos<RetornoTarefaDto>> AtualizarAsync(int tarefaId, CriarTarefaDto dto)
    {
        var tarefa = await _tarefaRepository.ObterPorIdAsync(tarefaId);

        if (tarefa == null)
        {
            return new RespostaMetodos<RetornoTarefaDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Tarefa não encontrada"
            };
        }

        if (dto.Prazo <= DateTime.UtcNow)
        {
            return new RespostaMetodos<RetornoTarefaDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "O prazo da tarefa deve ser uma data futura"
            };
        }

        if (dto.Pontos <= 0)
        {
            return new RespostaMetodos<RetornoTarefaDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Os pontos da tarefa devem ser maiores que zero"
            };
        }

        tarefa.Titulo = dto.Titulo;
        tarefa.Descricao = dto.Descricao;
        tarefa.Pontos = dto.Pontos;
        tarefa.Prazo = dto.Prazo;

        await _tarefaRepository.AtualizarAsync(tarefa);

        var retornoTarefa = tarefa.ToDto();

        return new RespostaMetodos<RetornoTarefaDto>
        {
            Sucesso = true,
            ObjetoRetorno = retornoTarefa,
            Mensagem = "Tarefa atualizada com sucesso"
        };
    }
    public async Task<RespostaMetodos<RetornoTarefaDto>> RemoverAsync(int tarefaId)
    {
        var tarefa = await _tarefaRepository.ObterPorIdAsync(tarefaId);
        if (tarefa == null)
        {
            return new RespostaMetodos<RetornoTarefaDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Tarefa não encontrada"
            };
        }

        await _tarefaRepository.RemoverAsync(tarefaId);

        return new RespostaMetodos<RetornoTarefaDto>
        {
            Sucesso = true,
            ObjetoRetorno = null,
            Mensagem = "Tarefa removida com sucesso"
        };
    }
}