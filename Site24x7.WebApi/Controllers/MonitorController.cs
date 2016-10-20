using Microsoft.AspNetCore.Mvc;

namespace Site24x7.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class MonitorController : Controller
    {
        // GET api/values/5
        [HttpGet]
        public string GetMonitors(string authToken)
        {
            Site24x7.Api.APMMonitor monitor = new Site24x7.Api.APMMonitor();
            return monitor.GetMonitors(authToken);
        }

        [HttpGet]
        public string RetrieveMonitor(string authToken, string monitorId)
        {
            Site24x7.Api.APMMonitor monitor = new Site24x7.Api.APMMonitor();
            return monitor.RetrieveMonitor(authToken, monitorId);
        }

        [HttpGet]
        public string RetrieveCurrentStatus(string authToken)
        {
            Site24x7.Api.APMMonitor monitor = new Site24x7.Api.APMMonitor();
            return monitor.RetrieveCurrentStatus(authToken);
        }

        [HttpPut]
        public string Suspend(string authToken, string monitorId)
        {
            Site24x7.Api.APMMonitor monitor = new Site24x7.Api.APMMonitor();
            return monitor.SuspendMonitor(authToken, monitorId);
        }

        [HttpPut]
        public string Activate(string authToken, string monitorId)
        {
            Site24x7.Api.APMMonitor monitor = new Site24x7.Api.APMMonitor();
            return monitor.ActivateMonitor(authToken, monitorId);
        }

        [HttpDelete]
        public string Delete(string authToken, string monitorId)
        {
            Site24x7.Api.APMMonitor monitor = new Site24x7.Api.APMMonitor();
            return monitor.DeleteMonitor(authToken, monitorId);
        }
    }
}
