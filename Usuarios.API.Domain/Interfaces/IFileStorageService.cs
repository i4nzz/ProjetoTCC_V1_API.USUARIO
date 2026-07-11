using Microsoft.AspNetCore.Http;

namespace GestaoTarefas.API.Domain.Interfaces;

public interface IFileStorageService
{
    Task<string> SalvarArquivoAsync(IFormFile arquivo, string subpasta);
    Task<(byte[] Conteudo, string ContentType)?> ObterArquivoAsync(string caminhoRelativo);
}
