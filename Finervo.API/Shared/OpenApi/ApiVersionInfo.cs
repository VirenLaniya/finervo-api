using Asp.Versioning;

namespace Finervo.API.Shared.OpenApi
{
    public sealed record ApiVersionInfo(string GroupName, string Title, ApiVersion Version, string Description, bool Deprecated = false);
}
