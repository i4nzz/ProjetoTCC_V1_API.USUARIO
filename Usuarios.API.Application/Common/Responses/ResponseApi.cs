namespace Usuarios.API.Application.Common.Responses
{
    public class ResponseApi<T>
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public T? Dados { get; set; }
        public List<string> Erros { get; set; }

        // sucesso com dados
        public ResponseApi(T dados, string mensagem = null!)
        {
            Sucesso = true;
            Mensagem = mensagem;
            Dados = dados;
            Erros = null!;
        }
        // erro com lista de mensagem
        public ResponseApi(List<string> erros)
        {
            Sucesso = false;
            Mensagem = "Ocorreu um erro na requisição";
            Dados = default;
            Erros = erros;
        }

        private ResponseApi(string mensagem, bool sucesso)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = default;
            Erros = sucesso ? null! : new List<string> { mensagem };
        }

        // factory methods
        public static ResponseApi<T> Ok(string mensagem) => new ResponseApi<T>(mensagem, true);

        public static ResponseApi<T> Erro(string mensagem) => new ResponseApi<T>(mensagem, false);

    }
}
