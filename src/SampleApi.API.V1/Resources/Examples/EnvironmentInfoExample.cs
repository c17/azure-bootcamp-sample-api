using System;
using Swashbuckle.AspNetCore.Filters;

namespace SampleApi.API.V1.Resources.Examples
{
    internal class EnvironmentInfoExample : IExamplesProvider<EnvironmentInfo>
    {
        public EnvironmentInfo GetExamples() =>
            new EnvironmentInfo(
                "srv-01",
                "srv-01",
                new[] { "10.0.0.1" },
                new DateTimeOffset(2020, 2, 20, 20, 20, 20, 20, TimeSpan.FromHours(2)),
                OperatingSystemInfo.ForHost(),
                Environment.Version.ToString(),
                Environment.ProcessorCount);
    }
}
