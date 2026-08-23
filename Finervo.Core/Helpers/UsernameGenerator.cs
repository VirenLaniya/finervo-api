
namespace Finervo.Core.Helpers
{
    public static class UsernameGenerator
    {
        private static readonly Random _random = Random.Shared;

        /// <summary>
        /// Generates a username from first and last name with a random numeric suffix.
        /// e.g. "John", "Smith" → "john.smith_1212"
        /// </summary>
        public static string Generate(string firstName, string lastName)
        {
            string cleanFirst = Sanitize(firstName);
            string cleanLast = Sanitize(lastName);
            int suffix = _random.Next(1000, 9999);

            return $"{cleanFirst}.{cleanLast}_{suffix}";
        }

        /// <summary>
        /// Generates a fallback username from email prefix with random suffix.
        /// e.g. "john@gmail.com" → "john_4829"
        /// </summary>
        public static string GenerateFromEmail(string email)
        {
            var prefix = email.Split('@')[0];
            var clean = Sanitize(prefix);
            var suffix = _random.Next(1000, 9999);

            return $"{clean}_{suffix}";
        }

        private static string Sanitize(string value) =>
            new([.. value
                .ToLowerInvariant()
                .Where(c => char.IsLetterOrDigit(c) || c == '_')]);
    }
}
