using Finervo.Contracts.Responses.User;
using Finervo.Core.Entities;
using Finervo.Core.Interfaces;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Queries.GetUsers
{
    public class GetUsersHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, Result<IEnumerable<UserResponseDto>>>
    {
        public async Task<Result<IEnumerable<UserResponseDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Core.Entities.User> users = await userRepository.GetAllAsync();

                List<UserResponseDto> userResponseDtos = users.Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    UserName = u.UserName,
                    CreatedAt = u.CreatedAt,
                    LastUpdatedAt = u.LastUpdatedAt,
                }).ToList();

                return Result<IEnumerable<UserResponseDto>>.Success(userResponseDtos);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserResponseDto>>.Failure(new Error("User.UnableToFetchUsers", ex.Message));
            }
        }
    }
}
