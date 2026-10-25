using Finervo.Core.Errors;
using Finervo.Core.Events;
using Finervo.Core.Primitives;
using System.Security.Cryptography;

namespace Finervo.Core.Entities
{
    public class User : AggregateRoot
    {
        private readonly List<UserRole> _userRoles = [];

        #region Fields

        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string UserName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string Password { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }

        // Auth fields
        public string? RefreshToken { get; private set; }
        public DateTime? RefreshTokenExpiryTime { get; private set; }

        public bool IsEmailVerified { get; private set; }
        public string? EmailVerificationToken { get; private set; }
        public DateTime? EmailVerificationTokenExpiry { get; private set; }

        #endregion

        #region Navigation Properties

        public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();

        #endregion

        #region Contructor
        private User(Guid id, string firstName, string lastName, string userName, string email, string password) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;
            LastUpdatedAt = DateTime.UtcNow;
            RefreshToken = null;
            RefreshTokenExpiryTime = null;
            IsEmailVerified = false;
            EmailVerificationToken = null;
            EmailVerificationTokenExpiry = null;
        }
        #endregion

        #region Methods
        public static Result<User> Create(string firstName,
            string lastName,
            string email,
            string userName,
            string passwordHash,
            bool emailVerificationRequired = true)
        {
            var user = new User(Guid.NewGuid(), firstName, lastName, userName, email, passwordHash);

            if (emailVerificationRequired)
            {
                user.IsEmailVerified = false;
                user.EmailVerificationToken = GenerateVerificationToken();
                user.EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24); // Token valid for 24 hours

                user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.Id, user.Email, user.FirstName, user.EmailVerificationToken, (DateTime)user.EmailVerificationTokenExpiry));
            }
            else
            {
                user.IsEmailVerified = true;

                user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id, user.Email));
            }

            return Result<User>.Success(user);
        }

        public Result UpdateProfile(
            string firstName,
            string lastName,
            string email,
            string userName)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            LastUpdatedAt = DateTime.UtcNow;

            return Result.Success();
        }

        public void SetRefreshToken(string refreshToken, DateTime expiryTime)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = expiryTime;
        }

        public void CleanRefreshToken()
        {
            RefreshToken = null;
            RefreshTokenExpiryTime = null;
        }

        public Result AssignRole(Guid roleId, Guid assignedBy)
        {
            if (_userRoles.Any(ur => ur.RoleId == roleId))
                return Result.Failure(UserErrors.RoleAlreadyAssigned);

            _userRoles.Clear(); // As of now we are not allowing multiple roles, so clear the existing one before assigning

            _userRoles.Add(UserRole.Create(Id, roleId, assignedBy));

            RaiseDomainEvent(new UserRoleAssignedDomainEvent(Id, roleId, assignedBy));

            return Result.Success();
        }

        public Result VerifyEmail(string token)
        {
            // Already verified
            if (IsEmailVerified)
                return Result.Failure(UserErrors.EmailAlreadyVerified);

            // Token mismatch
            if (EmailVerificationToken != token)
                return Result.Failure(UserErrors.InvalidVerificationToken);

            // Token expired
            if (EmailVerificationTokenExpiry < DateTime.UtcNow)
                return Result.Failure(UserErrors.VerificationTokenExpired);

            IsEmailVerified = true;
            EmailVerificationToken = null;
            EmailVerificationTokenExpiry = null;
            LastUpdatedAt = DateTime.UtcNow;

            RaiseDomainEvent(new UserEmailVerifiedDomainEvent(Id, Email, FirstName));

            return Result.Success();
        }

        public Result ResendVerification()
        {
            if (IsEmailVerified)
                return Result.Failure(UserErrors.EmailAlreadyVerified);

            EmailVerificationToken = GenerateVerificationToken();
            EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24);

            RaiseDomainEvent(new UserVerificationResendDomainEvent(
                Id,
                Email,
                FirstName,
                EmailVerificationToken,
                EmailVerificationTokenExpiry.Value));

            return Result.Success();
        }

        #endregion

        #region Helper Methods

        private static string GenerateVerificationToken()
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");   // ← URL safe base64
        }

        #endregion
    }
}
