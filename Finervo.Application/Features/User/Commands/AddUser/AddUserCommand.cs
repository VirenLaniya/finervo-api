using Finervo.Contracts.Requests.User;
using Finervo.Contracts.Responses.User;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Commands.AddUser
{
    public sealed record AddUserCommand(
        string FirstName,
        string LastName,
        string UserName, 
        string Email,
        string Password,
        string ConfirmPassword) : IRequest<Result<UserResponseDto>>
    {
        // Factory method to map request DTO fields with command fields
        public static AddUserCommand FromRequest(AddUserRequestDto dto) =>
            new(
                dto.FirstName,
                dto.LastName,
                dto.UserName,
                dto.Email,
                dto.Password,
                dto.ConfirmPassword);
    }
}
