using Finervo.Contracts.Requests.User;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Commands.UpdateMyProfile
{
    public sealed record UpdateMyProfileCommand(Guid Id, string FirstName, string LastName, string UserName, string Email) : IRequest<Result<bool>>
    {
        // Factory method to map request DTO fields with command fields
        public static UpdateMyProfileCommand FromRequest(Guid id, UpdateMyProfileRequestDto dto) =>
            new(
                id,
                dto.FirstName,
                dto.LastName,
                dto.UserName,
                dto.Email);
    }
}
