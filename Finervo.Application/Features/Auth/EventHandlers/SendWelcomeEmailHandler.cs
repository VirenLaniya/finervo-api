using Finervo.Application.Common.Interfaces;
using Finervo.Core.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Auth.EventHandlers
{
    public class SendWelcomeEmailHandler(IEmailService emailService, IClientUrlService clientUrlService) : INotificationHandler<UserEmailVerifiedDomainEvent>
    {
        public async Task Handle(UserEmailVerifiedDomainEvent notification, CancellationToken ct)
        {
            var loginLink = clientUrlService.GetLoginUrl();

            await emailService.SendWelcomeEmailAsync(
                notification.Email,
                notification.FirstName,
                loginLink,
                ct);
        }
    }
}
