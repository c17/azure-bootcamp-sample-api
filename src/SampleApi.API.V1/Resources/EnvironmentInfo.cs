using System;
using System.Collections.Generic;

namespace SampleApi.API.V1.Resources
{
    /// <summary>
    /// Environment information
    /// </summary>
    public class EnvironmentInfo
    {
        /// <inheritdoc/>
        public EnvironmentInfo(
            string machineName,
            string hostName,
            IEnumerable<string> ipAddresses,
            DateTimeOffset currentDate,
            OperatingSystemInfo operatingSystem,
            string clrVersion,
            int processorCount)
        {
            this.MachineName = machineName;
            this.HostName = hostName;
            this.IpAddresses = ipAddresses;
            this.CurrentDate = currentDate;
            this.OperatingSystem = operatingSystem ?? OperatingSystemInfo.Empty;
            this.ClrVersion = clrVersion;
            this.ProcessorCount = processorCount;
        }

        /// <summary>
        /// NetBIOS name
        /// </summary>
        public string MachineName { get; }
        /// <summary>
        /// Host name
        /// </summary>
        public string HostName { get; }
        /// <summary>
        /// List of IP addresses
        /// </summary>
        public IEnumerable<string> IpAddresses { get; }
        /// <summary>
        /// Current date and time (including offset)
        /// </summary>
        public DateTimeOffset CurrentDate { get; }
        /// <summary>
        /// Operating System
        /// </summary>
        public OperatingSystemInfo OperatingSystem { get; }
        /// <summary>
        /// CLR version
        /// </summary>
        public string ClrVersion { get; }
        /// <summary>
        /// Number of processors
        /// </summary>
        public int ProcessorCount { get; }
    }
}
