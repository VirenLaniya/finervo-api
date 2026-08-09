
namespace Finervo.Shared.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts PascalCase or any string to camelCase
        /// e.g. "UserName" → "userName"
        /// </summary>
        public static string ToCamelCase(this string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return value;

            if (char.IsLower(value[0]))
                return value;

            return char.ToLowerInvariant(value[0]) + value[1..];
        }
    }
}
