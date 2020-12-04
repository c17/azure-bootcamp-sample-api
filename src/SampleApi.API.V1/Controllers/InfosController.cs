using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.API.V1.Resources;

namespace SampleApi.API.V1.Controllers
{
    /// <summary>
    /// Provide information about hosting
    /// </summary>
    /// <remarks>/!\ Do NOT use it for real production apps</remarks>
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class InfosController : ControllerBase
    {
        /// <summary>
        /// Get Host information
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = Operations.Infos_Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EnvironmentInfo>> Get()
        {
            var hostname = Dns.GetHostName();
            var addresses = await Dns.GetHostAddressesAsync(hostname);
            return new EnvironmentInfo(
                Environment.MachineName,
                hostname,
                addresses.Select(a => a.ToString()),
                DateTimeOffset.Now,
                OperatingSystemInfo.ForHost(),
                Environment.Version.ToString(),
                Environment.ProcessorCount);
        }
    }
}
