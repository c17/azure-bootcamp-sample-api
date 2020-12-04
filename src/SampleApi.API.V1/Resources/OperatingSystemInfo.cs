using System;

namespace SampleApi.API.V1.Resources
{
    /// <summary>
    /// Information about Operating System
    /// </summary>
    public class OperatingSystemInfo
    {
        /// <inheritdoc/>
        public OperatingSystemInfo(string identifier, string platform, string version, string semanticVersion)
        {
            this.Identifier = identifier;
            this.Platform = platform;
            this.Version = version;
            this.SemanticVersion = semanticVersion;
        }

        /// <summary>
        /// Identifier
        /// </summary>
        public string Identifier { get; }
        /// <summary>
        /// Platform
        /// </summary>
        public string Platform { get; }
        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; }
        /// <summary>
        /// Version (semantic format)
        /// </summary>
        public string SemanticVersion { get; }

        internal static OperatingSystemInfo Empty = new OperatingSystemInfo(null, null, null, null);

        internal static OperatingSystemInfo ForHost() =>
            new OperatingSystemInfo(
                Environment.OSVersion.VersionString,
                Environment.OSVersion.Platform.ToString(),
                Environment.OSVersion.Version.ToString(),
                Environment.OSVersion.Version.ToSemanticVersion()
                );
    }
}
