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

        public static readonly Error RoleAlreadyAssigned =
            Error.Conflict("User.RoleAlreadyAssigned", "This role is already assigned to the user");

        public static readonly Error RoleNotAssigned =
            Error.BadRequest("User.RoleNotAssigned", "This role is not assigned to the user");

        public static readonly Error CannotRemoveLastSuperAdmin =
            Error.BadRequest("User.CannotRemoveLastSuperAdmin", "Cannot remove SuperAdmin role — at least one SuperAdmin must exist");

        public static readonly Error EmailAlreadyVerified =
            Error.BadRequest("User.EmailAlreadyVerified", "Email address is already verified");

        public static readonly Error InvalidVerificationToken =
            Error.BadRequest("User.InvalidVerificationToken", "Verification token is invalid");

        public static readonly Error VerificationTokenExpired =
            Error.BadRequest("User.VerificationTokenExpired", "Verification token has expired. Please request a new one");

        public static readonly Error EmailNotVerified =
            Error.Unauthorized("User.EmailNotVerified", "Please verify your email address before logging in. Check your inbox for the verification link.");
    }
}
