using System;

namespace Site24x7.Web.Models
{
    public class CurrentStatus
    {
        public long MonitorId { get; set; }
        public string MonitorIdString
        {
            get
            {
                return MonitorId.ToString();
            }
        }
        public string MonitorName { get; set; }
        public int Status { get; set; }
        public DateTime LastPolledTime { get; set; }
        public string DownReason { get; set; }
    }
}
