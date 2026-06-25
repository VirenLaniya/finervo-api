using Finervo.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public Result UpdateProfile(
        string firstName,
        string lastName,
        string email,
        string userName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure(new Error("User.InvalidFirstName", "Invalid First Name."));

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                return Result.Failure(new Error("User.InvalidEmail", "Invalid Email."));

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            LastUpdatedAt = DateTime.UtcNow;

            return Result.Success();
        }
    }
}
