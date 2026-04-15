using System.Net;

namespace Usuarios.API.Application.Common.Responses;
public class ResponseApi<T>
{
    public bool Sucesso { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string? Mensagem { get; set; }
    public T? ObjetoRetorno { get; set; }
    public List<string>? Erros { get; set; }

    /// sucesso
    public static ResponseApi<T> SucessoResponse(T dados, HttpStatusCode statusCode, string? mensagem = null)
    {
        return new ResponseApi<T>
        {
            Sucesso = true,
            StatusCode = statusCode,
            Mensagem = mensagem,
            ObjetoRetorno = dados,
            Erros = null
        };
    }

    // erro
    public static ResponseApi<T> ErroResponse(string mensagem, HttpStatusCode statusCode)
    {
        return new ResponseApi<T>
        {
            Sucesso = false,
            StatusCode = statusCode,
            Mensagem = mensagem,
            ObjetoRetorno = default,
            Erros = new List<string> { mensagem }
        };
    }
}