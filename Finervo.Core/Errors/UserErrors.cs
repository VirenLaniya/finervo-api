using Finervo.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Errors
{
    public static class UserErrors
    {
        public static readonly Error NotFound =
            Error.NotFound("User.NotFound", "User does not exist");

        public static readonly Error InvalidEmail =
            Error.BadRequest("User.InvalidEmail", "Invalid email address");

        public static readonly Error EmailAlreadyExists =
            Error.Conflict("User.EmailAlreadyExists", "Email is already registered");

        public static readonly Error InvalidUsername =
            Error.BadRequest("User.InvalidUserName", "Empty or Invalid username");

        public static readonly Error UsernameAlreadyExists =
            Error.Conflict("User.UsernameAlreadyExists", "Username is already in use");

        public static readonly Error InvalidCredentials =
            Error.Unauthorized("User.InvalidCredentials", "Invalid email or password");

        public static readonly Error InvalidRefreshToken =
            Error.Unauthorized("User.InvalidRefreshToken", "Refresh token is invalid");

        public static readonly Error RefreshTokenExpired =
            Error.Unauthorized("User.RefreshTokenExpired", "Refresh token has expired, please login again");
    }
}
