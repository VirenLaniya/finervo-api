using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Contracts.Requests.Auth
{
    public sealed record ResendVerificationRequestDto(string Email);
}
