using System;
using System.Collections.Generic;

namespace SampleApi.Resources
{
    public class EnvironmentInfo
    {
        public EnvironmentInfo(string machineName, string hostName, IEnumerable<string> ipAddresses, DateTimeOffset currentDate)
        {
            this.MachineName = machineName;
            this.HostName = hostName;
            this.IpAddresses = ipAddresses;
            this.CurrentDate = currentDate;
        }

        public string MachineName { get; }
        public string HostName { get; }
        public IEnumerable<string> IpAddresses { get; }
        public DateTimeOffset CurrentDate { get; }
    }
}
