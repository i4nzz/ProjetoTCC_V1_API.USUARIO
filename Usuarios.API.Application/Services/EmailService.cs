using System.Net;
using System.Text.RegularExpressions;
using GestaoTarefas.API.Application.Configuration;
using GestaoTarefas.API.Application.DTOs.Email;
using GestaoTarefas.API.Application.Interfaces;
using GestaoTarefas.Application.Common.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Resend;

namespace GestaoTarefas.API.Application.Services;

public class EmailService : IEmailService
{

    private readonly IResend _resend;
    private readonly ResendOptions _resendOptions;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IResend resend,
        IOptions<ResendOptions> options,
        ILogger<EmailService> logger)
    {
        _resend = resend;
        _resendOptions = options.Value;
        _logger = logger;
    }
    public async Task<RespostaMetodos<EmailResult>> EnviarAsync(SendEmailRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            ValidarRequisicao(request);

            var message = new EmailMessage
            {
                From = request.Remetente ?? _resendOptions.FormattedFrom,
                To = EmailAddressList.From(request.ListaDestinatarios),
                Subject = request.Assunto,
            };

            if (!string.IsNullOrWhiteSpace(request.HtmlBody))
            {
                message.HtmlBody = request.HtmlBody;
            }

            else if (!string.IsNullOrWhiteSpace(request.CorpoTexto))
            {
                message.TextBody = request.CorpoTexto;
            }

            else
            {
                throw new ArgumentException("É obrigatório fornecer HtmlBody ou TextBody.");
            }

            if (request.DestinatariosEmCopia is { Count: > 0 })
            {
                message.Cc = EmailAddressList.From(request.DestinatariosEmCopia);
            }

            if (request.DestinatariosEmCopiaOculta is { Count: > 0 })
            {
                message.Bcc = EmailAddressList.From(request.DestinatariosEmCopiaOculta);
            }

            if (request.Tags is { Count: > 0 })
            {
                message.Tags = request.Tags.Select(t => new EmailTag { Name = t.Key, Value = t.Value }).ToList();
            }

            _logger.LogInformation("Enviando e-mail | Para: {To} | Assunto: {Subject}", string.Join(", ", request.ListaDestinatarios), request.Assunto);

            var response = await _resend.EmailSendAsync(message, cancellationToken);

            _logger.LogInformation("E-mail enviado com sucesso | ID: {EmailId}", response.Content);

            var respostaEmail = EmailResult.Ok(response.Content.ToString());

            return new RespostaMetodos<EmailResult>
            {
                Sucesso = true,
                StatusCode = HttpStatusCode.OK,
                ObjetoRetorno = respostaEmail,
                Mensagem = "E-mail enviado com sucesso."
            };
        }
        catch (ResendException ex)
        {
            _logger.LogError(ex, "Erro do Resend ao enviar e-mail para {To}: {Error}", string.Join(", ", request.ListaDestinatarios), ex.Message);

            return new RespostaMetodos<EmailResult>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.BadRequest,
                ObjetoRetorno = EmailResult.Fail($"Erro do provedor: {ex.Message}"),
                Mensagem = "Falha ao enviar e-mail."
            };
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Dados inválidos na requisição de e-mail: {Error}", ex.Message);
            return new RespostaMetodos<EmailResult>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.BadRequest,
                ObjetoRetorno = EmailResult.Fail($"Dados inválidos: {ex.Message}"),
                Mensagem = "Falha ao enviar e-mail devido a dados inválidos."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao enviar e-mail para {To}", string.Join(", ", request.ListaDestinatarios));
            return new RespostaMetodos<EmailResult>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ObjetoRetorno = EmailResult.Fail("Erro inesperado ao enviar e-mail."),
                Mensagem = "Falha ao enviar e-mail devido a um erro inesperado."
            };
        }
    }

    public async Task<RespostaMetodos<EmailResult>> EnviarEmailConfirmacaoAsync(string toEmail, string userName, string confirmationLink, CancellationToken cancellationToken = default)
    {
        var html = $"""
            <!DOCTYPE html>
            <html lang="pt-BR">
            <head><meta charset="UTF-8"></head>
            <body style="margin:0;padding:0;background:#f1f5f9;font-family:Arial,sans-serif;">
              <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                  <td align="center" style="padding:40px 16px;">
                    <table width="600" cellpadding="0" cellspacing="0"style="background:#ffffff;border-radius:12px;overflow:hidden;box-shadow:0 2px 8px rgba(0,0,0,0.08);">
                      <tr>
                        <td style="background:#1E3A5F;padding:32px;text-align:center;">
                          <h1 style="color:#ffffff;margin:0;font-size:24px;">📧 TaskiDs</h1>
                        </td>
                      </tr>
                      <tr>
                        <td style="padding:40px 32px;">
                          <h2 style="color:#1E3A5F;margin:0 0 16px;">
                            Olá, {userName}! 👋
                          </h2>
                          <p style="color:#475569;font-size:16px;line-height:1.6;margin:0 0 24px;">
                            Obrigado por se cadastrar no <strong>TaskiDs</strong>!
                            Clique no botão abaixo para confirmar seu endereço de e-mail
                            e ativar sua conta.
                          </p>
                          <div style="text-align:center;margin:32px 0;">
                            <a href="{confirmationLink}"
                               style="background:#2D8CF0;color:#ffffff;padding:14px 32px;border-radius:8px;text-decoration:none;font-size:16px;font-weight:bold;display:inline-block;">
                              ✅ Confirmar E-mail
                            </a>
                          </div>
                          <p style="color:#94a3b8;font-size:13px;text-align:center;">
                            Este link expira em <strong>24 horas</strong>.<br>
                            Se você não criou uma conta, ignore este e-mail.
                          </p>
                        </td>
                      </tr>
                      <tr>
                        <td style="background:#f8fafc;padding:20px 32px;text-align:center;border-top:1px solid #e2e8f0;">
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </body>
            </html>
            """;

        var respostaEnviarEmail = await EnviarAsync(new SendEmailRequest
        {
            ListaDestinatarios = [toEmail],
            Assunto = "Confirme seu e-mail",
            HtmlBody = html,
            Tags = new Dictionary<string, string>
            {
                ["type"] = "confirmation",
                ["version"] = "v1"
            }
        }, cancellationToken);

        if (respostaEnviarEmail.Sucesso)
        {
            return new RespostaMetodos<EmailResult>
            {
                Sucesso = true,
                StatusCode = HttpStatusCode.OK,
                ObjetoRetorno = respostaEnviarEmail.ObjetoRetorno,
                Mensagem = "E-mail de confirmação enviado com sucesso."
            };
        }
        return new RespostaMetodos<EmailResult>
        {
            Sucesso = false,
            StatusCode = respostaEnviarEmail.StatusCode,
            ObjetoRetorno = respostaEnviarEmail.ObjetoRetorno,
            Mensagem = "Falha ao enviar e-mail de confirmação."
        };

    }

    public async Task<RespostaMetodos<EmailResult>> EnviarEmailResetSenhaAsync(string toEmail, string userName, string resetLink, CancellationToken cancellationToken = default)
    {
        var html = $"""
            <!DOCTYPE html>
            <html lang="pt-BR">
            <head><meta charset="UTF-8"></head>
            <body style="margin:0;padding:0;background:#f1f5f9;font-family:Arial,sans-serif;">
              <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                  <td align="center" style="padding:40px 16px;">
                    <table width="600" cellpadding="0" cellspacing="0"style="background:#ffffff;border-radius:12px;overflow:hidden;box-shadow:0 2px 8px rgba(0,0,0,0.08);">

                      <tr>
                        <td style="background:#1E3A5F;padding:32px;text-align:center;">
                          <h1 style="color:#ffffff;margin:0;font-size:24px;">🔐 TaskiDs</h1>
                        </td>
                      </tr>

                      <tr>
                        <td style="padding:40px 32px;">
                          <h2 style="color:#1E3A5F;margin:0 0 16px;">
                            Redefinição de senha
                          </h2>
                          <p style="color:#475569;font-size:16px;line-height:1.6;margin:0 0 8px;">
                            Olá, <strong>{userName}</strong>!
                          </p>
                          <p style="color:#475569;font-size:16px;line-height:1.6;margin:0 0 24px;">
                            Recebemos uma solicitação para redefinir a senha da sua conta.
                            Clique no botão abaixo para continuar:
                          </p>
                          <div style="text-align:center;margin:32px 0;">
                            <a href="{resetLink}"
                               style="background:#EA580C;color:#ffffff;padding:14px 32px;
                                      border-radius:8px;text-decoration:none;font-size:16px;
                                      font-weight:bold;display:inline-block;">
                              🔑 Redefinir Senha
                            </a>
                          </div>
                          <div style="background:#FFF7ED;border-left:4px solid #EA580C;
                                      padding:12px 16px;border-radius:4px;margin-bottom:24px;">
                            <p style="color:#9a3412;font-size:13px;margin:0;">
                              ⚠️ Este link expira em <strong>1 hora</strong>.
                              Se não foi você quem solicitou, ignore este e-mail —
                              sua senha permanece a mesma.
                            </p>
                          </div>
                        </td>
                      </tr>

                      <tr>
                        <td style="background:#f8fafc;padding:20px 32px;text-align:center;border-top:1px solid #e2e8f0;">
                        </td>
                      </tr>

                    </table>
                  </td>
                </tr>
              </table>
            </body>
            </html>
            """;

        var respostaEnviarEmail = await EnviarAsync(new SendEmailRequest
        {
            ListaDestinatarios = [toEmail],
            Assunto = "Redefinição de senha",
            HtmlBody = html,
            Tags = new Dictionary<string, string>
            {
                ["type"] = "password-reset",
                ["version"] = "v1"
            }
        }, cancellationToken);

        if (respostaEnviarEmail.Sucesso)
        {
            return new RespostaMetodos<EmailResult>
            {
                Sucesso = true,
                StatusCode = HttpStatusCode.OK,
                ObjetoRetorno = respostaEnviarEmail.ObjetoRetorno,
                Mensagem = "E-mail de redefinição de senha enviado com sucesso."
            };
        }

        return new RespostaMetodos<EmailResult>
        {
            Sucesso = false,
            StatusCode = respostaEnviarEmail.StatusCode,
            ObjetoRetorno = respostaEnviarEmail.ObjetoRetorno,
            Mensagem = "Falha ao enviar e-mail de redefinição de senha."
        };

    }

    public async Task<RespostaMetodos<EmailResult>> EnviarNotificacaoSistemaAsync(string toEmail, string subject, string message, CancellationToken cancellationToken = default)
    {
        var html = $"""
            <!DOCTYPE html>
            <html lang="pt-BR">
            <head><meta charset="UTF-8"></head>
            <body style="margin:0;padding:0;background:#f1f5f9;font-family:Arial,sans-serif;">
              <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                  <td align="center" style="padding:40px 16px;">
                    <table width="600" cellpadding="0" cellspacing="0"style="background:#ffffff;border-radius:12px;overflow:hidden;">

                      <tr>
                        <td style="background:#1E3A5F;padding:24px 32px;">
                          <h2 style="color:#ffffff;margin:0;font-size:18px;">
                            🔔 Notificação
                          </h2>
                        </td>
                      </tr>

                      <tr>
                        <td style="padding:32px;">
                          <p style="color:#1e293b;font-size:16px;line-height:1.6;margin:0;">
                            {message}
                          </p>
                        </td>
                      </tr>

                      <tr>
                        <td style="background:#f8fafc;padding:16px 32px;border-top:1px solid #e2e8f0;text-align:center;">
                        </td>
                      </tr>

                    </table>
                  </td>
                </tr>
              </table>
            </body>
            </html>
            """;

        var respostaEnviarEmail = await EnviarAsync(new SendEmailRequest
        {
            ListaDestinatarios = [toEmail],
            Assunto = subject,
            HtmlBody = html,
            Tags = new Dictionary<string, string>
            {
                ["type"] = "system-notification"
            }
        }, cancellationToken);

        if (respostaEnviarEmail.Sucesso)
        {
            return new RespostaMetodos<EmailResult>
            {
                StatusCode = HttpStatusCode.OK,
                ObjetoRetorno = respostaEnviarEmail.ObjetoRetorno,
                Mensagem = "Notificação enviada com sucesso.",
            };
        }

        return new RespostaMetodos<EmailResult>
        {
            Sucesso = false,
            StatusCode = respostaEnviarEmail.StatusCode,
            ObjetoRetorno = respostaEnviarEmail.ObjetoRetorno,
            Mensagem = "Falha ao enviar notificação."
        };
    }

    private static void ValidarRequisicao(SendEmailRequest request)
    {
        if (request.ListaDestinatarios is not { Count: > 0 })
        {
            throw new ArgumentException("Informe pelo menos um destinatário.", nameof(request.ListaDestinatarios));
        }

        if (string.IsNullOrWhiteSpace(request.Assunto))
        {
            throw new ArgumentException("O assunto é obrigatório.", nameof(request.Assunto));
        }

        if (string.IsNullOrWhiteSpace(request.CorpoTexto) && string.IsNullOrWhiteSpace(request.HtmlBody))
        {
            throw new ArgumentException("Informe CorpoTexto ou HtmlBody.");
        }

        foreach (var email in request.ListaDestinatarios)
        {
            if (!ValidarEmail(email))
            {
                throw new ArgumentException($"Endereço de e-mail inválido: '{email}'", nameof(request.ListaDestinatarios));
            }
        }

        if (request.DestinatariosEmCopia is not null)
        {
            foreach (var email in request.DestinatariosEmCopia)
            {
                if (!ValidarEmail(email))
                {
                    throw new ArgumentException($"Endereço de e-mail inválido em cópia: '{email}'", nameof(request.DestinatariosEmCopia));
                }
            }
        }

        if (request.DestinatariosEmCopiaOculta is not null)
        {
            foreach (var email in request.DestinatariosEmCopiaOculta)
            {
                if (!ValidarEmail(email))
                {
                    throw new ArgumentException($"Endereço de e-mail inválido em cópia oculta: '{email}'", nameof(request.DestinatariosEmCopiaOculta));
                }
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Remetente) && !ValidarEmail(request.Remetente))
        {
            throw new ArgumentException($"Endereço de remetente inválido: '{request.Remetente}'", nameof(request.Remetente));
        }
    }

    private static bool ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
    }
}
