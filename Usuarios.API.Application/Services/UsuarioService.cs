using Usuarios.API.Application.Common.Responses;
using Usuarios.API.Application.DTOs.Login;
using Usuarios.API.Application.DTOs.Usuario;
using Usuarios.API.Application.Interfaces;
using Usuarios.API.Application.Mapping;
using Usuarios.API.Domain.Entities;
using Usuarios.API.Domain.Enum;
using Usuarios.API.Domain.Interfaces;

namespace Usuarios.API.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;

    public UsuarioService(
        IUsuarioRepository usuarioRepository
        , ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
    }

    public async Task<RespostaMetodos<IEnumerable<RetornoUsuarioDto>>> ObterTodosAsync()
    {
        var usuarios = await _usuarioRepository.ObterTodosAsync();

        if (usuarios == null || !usuarios.Any())
        {
            return new RespostaMetodos<IEnumerable<RetornoUsuarioDto>>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Nenhum usuário encontrado"
            };
        }

        var retornoUsuarios = usuarios.ToDtoList();

        return new RespostaMetodos<IEnumerable<RetornoUsuarioDto>>
        {
            Sucesso = true,
            ObjetoRetorno = retornoUsuarios,
            Mensagem = "Usuários obtidos com sucesso"
        };
    }

    public async Task<RespostaMetodos<RetornoUsuarioDto?>> ObterPorIdAsync(int id)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(id);

        if (usuario == null)
        {
            return new RespostaMetodos<RetornoUsuarioDto?>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Usuário não encontrado"
            };
        }

        var retornoUsuario = usuario.ToDto();

        return new RespostaMetodos<RetornoUsuarioDto?>
        {
            Sucesso = true,
            ObjetoRetorno = retornoUsuario,
            Mensagem = "Usuário obtido com sucesso"
        };
    }

    public async Task<RespostaMetodos<RetornoUsuarioDto>> CriarUsuarioAsync(CriarUsuarioDto dto)
    {
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
        var usuario = new Usuario(dto.Nome, dto.Email, senhaHash, PerfilUsuarioEnum.Pai);

        await _usuarioRepository.AdicionarAsync(usuario);

        var usuarioRetorno = usuario.ToDto();

        return new RespostaMetodos<RetornoUsuarioDto>
        {
            Sucesso = true,
            ObjetoRetorno = usuarioRetorno,
            Mensagem = "Usuário criado com sucesso"
        };
    }

    public async Task<RespostaMetodos<RetornoUsuarioDto>> AtualizarAsync(int id, AtualizarUsuarioDto dto)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(id);
        if (usuario == null)
        {
            return new RespostaMetodos<RetornoUsuarioDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Usuário não encontrado"
            };
        }


        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;

        await _usuarioRepository.AtualizarAsync(usuario);

        var usuarioRetorno = usuario.ToDto();

        return new RespostaMetodos<RetornoUsuarioDto>
        {
            Sucesso = true,
            ObjetoRetorno = usuarioRetorno,
            Mensagem = "Usuário atualizado com sucesso"
        };
    }

    public async Task<RespostaMetodos<RetornoUsuarioDto>> RemoverAsync(int id)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(id);

        if (usuario == null)
        {
            return new RespostaMetodos<RetornoUsuarioDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Usuário não encontrado"
            };
        }
        await _usuarioRepository.RemoverAsync(id);

        return new RespostaMetodos<RetornoUsuarioDto>
        {
            Sucesso = true,
            ObjetoRetorno = null,
            Mensagem = "Usuário removido com sucesso"
        };
    }

    public async Task<RespostaMetodos<RetornoUsuarioDto>> CriarFilhoAsync(CriarFilhoDto dto)
    {
        var pai = await _usuarioRepository.ObterPorIdAsync(dto.PaiId);

        if (pai == null)
        {
            return new RespostaMetodos<RetornoUsuarioDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Pai não encontrado"
            };
        }


        if (pai.Perfil != PerfilUsuarioEnum.Pai)
        {
            return new RespostaMetodos<RetornoUsuarioDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "O usuário informado não é um Pai"
            };
        }

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

        var filho = new Usuario(dto.Nome, dto.Email, senhaHash, PerfilUsuarioEnum.Filho)
        {
            PaiId = dto.PaiId
        };

        await _usuarioRepository.AdicionarAsync(filho);

        return new RespostaMetodos<RetornoUsuarioDto>
        {
            Sucesso = true,
            ObjetoRetorno = filho.ToDto(),
            Mensagem = "Filho cadastrado com sucesso"
        };
    }

    public async Task<RespostaMetodos<RetornoLoginDto>> LoginAsync(LoginDto dto)
    {
        var usuario = await _usuarioRepository.ObterPorEmailAsync(dto.Email);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
        {
            return new RespostaMetodos<RetornoLoginDto>
            {
                Sucesso = false,
                ObjetoRetorno = null,
                Mensagem = "Email ou senha inválidos"
            };
        }


        var token = _tokenService.GerarToken(usuario);

        return new RespostaMetodos<RetornoLoginDto>
        {
            Sucesso = true,
            ObjetoRetorno = new RetornoLoginDto
            {
                Token = token,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil.ToString(),
                Expiracao = DateTime.UtcNow.AddHours(8)
            },
            Mensagem = "Login realizado com sucesso"
        };
    }

}