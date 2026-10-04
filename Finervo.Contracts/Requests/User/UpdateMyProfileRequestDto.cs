using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Contracts.Requests.User
{
    public sealed record UpdateMyProfileRequestDto(string FirstName, string LastName, string UserName, string Email);
}
