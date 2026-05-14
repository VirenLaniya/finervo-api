using Asp.Versioning;

namespace Finervo.API.Shared.OpenApi
{
    public static class ApiVersions
    {
        public const string V1 = "v1";
        public const string V2 = "v2";

        // Numeric versions for AddApiVersioning
        public static class Numeric
        {
            public static readonly ApiVersion V1 = new(1, 0);
            public static readonly ApiVersion V2 = new(2, 0);
        }
    }
}
