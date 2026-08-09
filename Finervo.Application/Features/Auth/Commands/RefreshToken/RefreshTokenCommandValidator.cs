using FluentValidation;

namespace Finervo.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User Id is required");

            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh Token is required");
        }
    }
}
