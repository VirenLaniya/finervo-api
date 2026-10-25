using Finervo.Application.Common.Interfaces;
using Finervo.Core.Events;
using Finervo.Core.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Auth.EventHandlers
{
    public class SendVerificationEmailHandler(IEmailService emailService, IClientUrlService clientUrlService) : INotificationHandler<UserRegisteredDomainEvent>
    {
        public async Task Handle(
        UserRegisteredDomainEvent notification,
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
