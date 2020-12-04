using System;

namespace SampleApi.API.V1.Resources
{
    internal static class VersionExtensions
    {
        public static string ToSemanticVersion(this Version version) =>
            $"{version.Major}.{version.Minor}.{version.Revision}";
    }
}
