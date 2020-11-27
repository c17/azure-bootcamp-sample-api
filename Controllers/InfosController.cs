using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Resources;

namespace SampleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfosController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<EnvironmentInfo>> Get()
        {
            var hostname = Dns.GetHostName();
            var addresses = await Dns.GetHostAddressesAsync(hostname);
            return new EnvironmentInfo(
                Environment.MachineName,
                hostname,
                addresses.Select(a => a.ToString()),
                DateTimeOffset.Now);
        }
    }
}
