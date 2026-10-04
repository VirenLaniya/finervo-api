using Finervo.Contracts.Requests.Admin.User;
using Finervo.Contracts.Responses.User;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Admin.User.Commands.AddUser
{
    public sealed record AddUserCommand(
        string FirstName,
        string LastName,
        string UserName, 
        string Email,
        string Password,
        string ConfirmPassword,
        Guid RoleId,
        Guid AssignedBy) : IRequest<Result<UserResponseDto>>
    {
        // Factory method to map request DTO fields with command fields
        public static AddUserCommand FromRequest(AddUserRequestDto dto, Guid AssignedBy) =>
            new(
                dto.FirstName,
                dto.LastName,
                dto.UserName,
                dto.Email,
                dto.Password,
                dto.ConfirmPassword,
                dto.RoleId,
                AssignedBy);
    }
}
