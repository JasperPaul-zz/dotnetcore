using Site24x7Api.Util;
using System;

namespace Site24x7.Api
{
    public class APMMonitor
    {
        public string GetMonitors(string authToken)
        {
            string result = string.Empty;
            try
            {
                string query = string.Format("{0}={1}", "authtoken", authToken);
                string actionString = string.Format("{0}/{1}", "api", "monitors");
                var urlBuilder = HttpSendData.GetUrlBuilder(APIType.SITE24X7_API, actionString, query);
                var responseObj = HttpSendData.SendData(urlBuilder.Url, null, true, null, MethodType.GET);

                if (responseObj != null)
                {
                    responseObj.Wait();
                    result = responseObj.Result.ToString();
                }
                else
                {
                    result = "{\"error_code\":9999,\"message\":\"response is null.\"}";
                }

            }
            catch (Exception ex)
            {
                result = "{\"error\":9999,\"message\":\" " + ex.ToString() + " \"}";
            }
            return result;
        }

        public string RetrieveMonitor(string authToken, string monitorId)
        {
            string result = string.Empty;
            try
            {
                string query = string.Format("{0}={1}", "authtoken", authToken);
                string actionString = string.Format("{0}/{1}/{2}", "api", "monitors", monitorId);
                var urlBuilder = HttpSendData.GetUrlBuilder(APIType.SITE24X7_API, actionString, query);
                var responseObj = HttpSendData.SendData(urlBuilder.Url, null, true, null, MethodType.GET);

                if (responseObj != null)
                {
                    responseObj.Wait();
                    result = responseObj.Result.ToString();
                }
                else
                {
                    result = "{\"error_code\":9999,\"message\":\"response is null.\"}";
                }

            }
            catch (Exception ex)
            {
                result = "{\"error\":9999,\"message\":\" " + ex.ToString() + " \"}";
            }
            return result;
        }

        public string RetrieveCurrentStatus(string authToken)
        {
            string result = string.Empty;
            try
            {
                string query = string.Format("{0}={1}&{2}={3}&{4}={5}", "authtoken", authToken, "widget_required", "false", "apm_required", "true");
                string actionString = string.Format("{0}/{1}", "api", "current_status");
                var urlBuilder = HttpSendData.GetUrlBuilder(APIType.SITE24X7_API, actionString, query);
                var responseObj = HttpSendData.SendData(urlBuilder.Url, null, true, null, MethodType.GET);

                if (responseObj != null)
                {
                    responseObj.Wait();
                    result = responseObj.Result.ToString();
                }
                else
                {
                    result = "{\"error_code\":9999,\"message\":\"response is null.\"}";
                }

            }
            catch (Exception ex)
            {
                result = "{\"error\":9999,\"message\":\" " + ex.ToString() + " \"}";
            }
            return result;
        }

        public string SuspendMonitor(string authToken, string monitorId)
        {
            string actionString = string.Format("{0}/{1}/{2}/{3}", "api", "monitors", "suspend", monitorId);
            return ManageMonitor(authToken, monitorId, actionString, MethodType.PUT);
        }

        public string ActivateMonitor(string authToken, string monitorId)
        {
            string actionString = string.Format("{0}/{1}/{2}/{3}", "api", "monitors", "activate", monitorId);
            return ManageMonitor(authToken, monitorId, actionString, MethodType.PUT);
        }

        public string DeleteMonitor(string authToken, string monitorId)
        {
            string actionString = string.Format("{0}/{1}/{2}", "api", "monitors", monitorId);
            return ManageMonitor(authToken, monitorId, actionString, MethodType.DELETE);
        }

        private string ManageMonitor(string authToken, string monitorId, string actionString, MethodType methodType)
        {
            string result = string.Empty;
            try
            {
                string query = string.Format("{0}={1}", "authtoken", authToken);
                var urlBuilder = HttpSendData.GetUrlBuilder(APIType.SITE24X7_API, actionString, query);
                var responseObj = HttpSendData.SendData(urlBuilder.Url, null, true, null, methodType);

                if (responseObj != null)
                {
                    responseObj.Wait();
                    result = responseObj.Result.ToString();
                }
                else
                {
                    result = "{\"error_code\":9999,\"message\":\"response is null.\"}";
                }

            }
            catch (Exception ex)
            {
                result = "{\"error\":9999,\"message\":\" " + ex.ToString() + " \"}";
            }
            return result;
        }
    }
}
