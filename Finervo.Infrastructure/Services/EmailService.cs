using Finervo.Application.Common.Interfaces;
using Finervo.Infrastructure.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Services
{
    public class EmailService(IOptions<SmtpOptions> options, ILogger<EmailService> logger) : IEmailService
    {
        private readonly SmtpOptions _smtp = options.Value;

        public async Task SendEmailVerificationAsync(
            string toEmail,
            string toName,
            string verificationLink,
            DateTime tokenExpiry,
            CancellationToken ct = default)
        {
            var expiryFormatted = tokenExpiry.ToString("dd MMM yyyy HH:mm UTC");

            var body = $"""
            <h2>Welcome to Finervo, {toName}!</h2>
            <p>Please verify your email address to activate your account.</p>
            <p>
                <a href="{verificationLink}"
                   style="background:#6c63ff;color:#fff;padding:12px 24px;
                          border-radius:6px;text-decoration:none;display:inline-block;">
                    Verify Email Address
                </a>
            </p>
            <p style="color:#666;font-size:14px;">
                This link expires on <strong>{expiryFormatted}</strong>.
            </p>
            <p style="color:#666;font-size:14px;">
                If you did not create a Finervo account, you can safely ignore this email.
            </p>
            <hr/>
            <p style="color:#999;font-size:12px;">
                If the button doesn't work, copy and paste this link:<br/>
                <a href="{verificationLink}">{verificationLink}</a>
            </p>
            """;

            await SendAsync(toEmail, toName, "Verify your Finervo account", body, ct);
        }

        public async Task SendWelcomeEmailAsync(
            string toEmail,
            string toName,
            string loginLink,
            CancellationToken ct = default)
        {
            var subject = "Welcome to Finervo";

            var body = $"""
            <h2>Welcome to Finervo, {toName}!</h2>
            <p>Your account has been created successfully.</p>
            <p>Start tracking your expenses and managing your budget today.</p>
            <p>
                <a href="{loginLink}"
                   style="background:#6c63ff;color:#fff;padding:12px 24px;
                          border-radius:6px;text-decoration:none;">
                    Login to Finervo
                </a>
            </p>
            """;

            await SendAsync(toEmail, toName, subject, body, ct);
        }

        private async Task SendAsync(
        string toEmail,
        string toName,
        string subject,
        string htmlBody,
        CancellationToken ct)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_smtp.FromName, _smtp.FromEmail));
                message.To.Add(new MailboxAddress(toName, toEmail));
                message.Subject = subject;

                message.Body = new BodyBuilder
                {
                    HtmlBody = htmlBody
                }.ToMessageBody();

                using var client = new SmtpClient();

                await client.ConnectAsync(
                    _smtp.Host,
                    _smtp.Port,
                    _smtp.EnableSsl
                        ? SecureSocketOptions.StartTls
                        : SecureSocketOptions.None,
                    ct);

                await client.AuthenticateAsync(
                    _smtp.UserName,
                    _smtp.Password,
                    ct);

                await client.SendAsync(message, ct);
                await client.DisconnectAsync(true, ct);

                //logger.LogInformation(
                //    "Email sent to {ToEmail} — Subject: {Subject}",
                //    toEmail,
                //    subject);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "Failed to send email to {ToEmail} — Subject: {Subject}",
                    toEmail,
                    subject);
            }
        }
    }
}
