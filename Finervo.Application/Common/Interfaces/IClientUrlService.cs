using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Common.Interfaces
{
    public interface IClientUrlService
    {
        string GetEmailVerificationUrl(Guid userId, string token);
        string GetLoginUrl();
    }
}
