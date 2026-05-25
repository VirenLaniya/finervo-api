namespace Finervo.API.Shared.OpenApi
{
    public static class ApiVersionProvider
    {
        private const string _defaultApiVersion = ApiVersions.V2;

        public static IReadOnlyList<ApiVersionInfo> _versions = [
            new(GroupName: ApiVersions.V1, Title: "Finervo API", Version: ApiVersions.Numeric.V1, Description: "Finervo Initial Release", Deprecated: false),
            new(GroupName: ApiVersions.V2, Title: "Finervo API - Version 2", Version: ApiVersions.Numeric.V2, Description: "Finervo API Version 2 Release", Deprecated: false)
        ];

        // Fetch All Api Versions
        public static IReadOnlyList<ApiVersionInfo> GetApiVersions() => _versions;

        // Fetch Active Api Versions
        public static IReadOnlyList<ApiVersionInfo> GetActiveApiVersions() => _versions.Where(v => !v.Deprecated).ToList();

        // Fetch Default Api Version
        public static ApiVersionInfo GetDefaultApiVersion() => _versions.FirstOrDefault(v => v.GroupName == _defaultApiVersion) ?? _versions.First();
    }
}
