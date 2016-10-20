namespace Site24x7.Web.Models
{
    public class Monitor
    {
        public long MonitorId { get; set; }
        public string MonitorIdString
        {
            get
            {
                return MonitorId.ToString();
            }
        }
        public int Status { get; set; }
        public string MonitorName { get; set; }
        public string MonitorType { get; set; }
    }
}
