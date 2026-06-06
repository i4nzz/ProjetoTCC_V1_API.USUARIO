using GestaoTarefas.API.Application.DTOs.Email;
using GestaoTarefas.Application.Common.Responses;

namespace GestaoTarefas.API.Application.Interfaces;

public interface IEmailService
{
    Task<RespostaMetodos<EmailResult>> EnviarAsync(SendEmailRequest request, CancellationToken cancellationToken = default);
    Task<RespostaMetodos<EmailResult>> EnviarEmailConfirmacaoAsync(string toEmail, string userName, string confirmationLink, CancellationToken cancellationToken = default);
    Task<RespostaMetodos<EmailResult>> EnviarEmailResetSenhaAsync(string toEmail, string userName, string resetLink, CancellationToken cancellationToken = default);
    Task<RespostaMetodos<EmailResult>> EnviarNotificacaoSistemaAsync(string toEmail, string subject, string message, CancellationToken cancellationToken = default);
}
