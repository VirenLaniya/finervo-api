using Finervo.Application.Common.Interfaces;
using Finervo.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Services
{
    internal sealed class ClientUrlService(IOptions<ClientOptions> options) : IClientUrlService
    {
        private readonly ClientOptions _client = options.Value;

        public string GetEmailVerificationUrl(Guid userId, string token) =>
            $"{_client.EmailVerificationUrl}" +
            $"?userId={userId}" +
            $"&token={Uri.EscapeDataString(token)}";

        public string GetLoginUrl() => $"{_client.LoginUrl}";
    }
}
