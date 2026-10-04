using Finervo.Core.Errors;
using Finervo.Core.Events;
using Finervo.Core.Primitives;

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
        }
        #endregion

        #region Methods
        public static Result<User> Create(string firstName,
            string lastName,
            string email,
            string userName,
            string passwordHash)
        {
            var user = new User(Guid.NewGuid(), firstName, lastName, userName, email, passwordHash);

            user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.Id, user.Email));

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

        #endregion
    }
}
