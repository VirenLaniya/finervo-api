using Finervo.Contracts.Responses.User;
using Finervo.Core.Errors;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Queries.GetMyProfile
{
    public class GetMyProfileQueryHandler(IUserRepository userRepository) : IRequestHandler<GetMyProfileQuery, Result<UserResponseDto>>
    {
        public async Task<Result<UserResponseDto>> Handle(GetMyProfileQuery request, CancellationToken ct)
        {
            try
            {

                var user = await userRepository.GetByIdAsync(request.Id, ct);

                if (user is null)
                    return Result<UserResponseDto>.Failure(UserErrors.NotFound);

                UserResponseDto userResponseDtos = new()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    CreatedAt = user.CreatedAt,
                    LastUpdatedAt = user.LastUpdatedAt,
                };

                return Result<UserResponseDto>.Success(userResponseDtos);
            }
            catch (Exception ex)
            {
                return Result<UserResponseDto>.Failure(new Error("User.UnableToFetchUser", ex.Message));
            }
        }
    }
}
