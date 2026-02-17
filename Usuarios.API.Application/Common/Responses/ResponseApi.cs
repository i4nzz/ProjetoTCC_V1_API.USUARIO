namespace Usuarios.API.Application.Common.Responses
{
    public class ResponseApi<T> where T : class
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public T? Dados { get; set; }
        public List<string> Erros { get; set; }

        public ResponseApi(T dados, string mensagem = null!)
        {
            Sucesso = true;
            Mensagem = mensagem;
            Dados = dados;
            Erros = null!;
        }

        public ResponseApi(List<string> erros)
        {
            Sucesso = false;
            Erros = erros;
        }


    }
}
