using Finervo.Application.Common.Interfaces;
using Finervo.Core.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Auth.EventHandlers
{
    public sealed class SendResendVerificationEmailHandler(IEmailService emailService, IClientUrlService clientUrlService)
        : INotificationHandler<UserVerificationResendDomainEvent>
    {
        public async Task Handle(
            UserVerificationResendDomainEvent notification,
            CancellationToken ct)
        {
            var verificationLink = clientUrlService.GetEmailVerificationUrl(
                notification.UserId,
                notification.VerificationToken);

            await emailService.SendEmailVerificationAsync(
                notification.Email,
                notification.FirstName,
                verificationLink,
                notification.TokenExpiry,
                ct);
        }
    }
}
