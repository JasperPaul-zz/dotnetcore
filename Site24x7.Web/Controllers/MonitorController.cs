using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Site24x7.Web.Models;
using Site24x7.Web.Util;
using System.Collections.Generic;

namespace Site24x7.Web.Controllers
{
    public class MonitorController : ParentController
    {
        public MonitorController(IOptions<AppSettings> settings) : base(settings)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListMonitors()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetMonitors()
        {
            List<Monitor> monitors = new List<Monitor>();
            string query = string.Format("{0}={1}", "authtoken", AuthToken);
            string apiurl = GeneralUtility.GetAPIUrl(AppSettings.APIHost, AppSettings.APIPort, "monitor", "getmonitors", query);
            var responseObj = GeneralUtility.SendData(apiurl, null, false, null, MethodType.GET);
            if (responseObj != null)
            {
                responseObj.Wait();
                var result = responseObj.Result.ToString();
                object obj = JSONParser.ToObject(result);
                monitors = APMSerializer.GetMonitors(obj);
            }

            return Json(monitors);
        }

        [HttpGet]
        public JsonResult GetCurrentStatus()
        {
            List<CurrentStatus> currentStatusList = new List<CurrentStatus>();
            string query = string.Format("{0}={1}", "authtoken", AuthToken);
            string apiurl = GeneralUtility.GetAPIUrl(AppSettings.APIHost, AppSettings.APIPort, "monitor", "retrievecurrentstatus", query);
            var responseObj = GeneralUtility.SendData(apiurl, null, false, null, MethodType.GET);
            if (responseObj != null)
            {
                responseObj.Wait();
                var result = responseObj.Result.ToString();
                object obj = JSONParser.ToObject(result);
                currentStatusList = APMSerializer.GetCurrentStatus(obj);
            }

            return Json(currentStatusList);
        }

        public IActionResult CurrentStatus()
        {
            return View();
        }

        [HttpPut]
        public JsonResult Suspend(string monitorId)
        {
            ResponseStatus rs = new ResponseStatus();
            string query = string.Format("authtoken={0}&monitorid={1}", AuthToken, monitorId);
            string apiurl = GeneralUtility.GetAPIUrl(AppSettings.APIHost, AppSettings.APIPort, "monitor", "suspend", query);
            var responseObj = GeneralUtility.SendData(apiurl, null, false, null, MethodType.PUT);
            if (responseObj != null)
            {
                responseObj.Wait();
                var result = responseObj.Result.ToString();
                object obj = JSONParser.ToObject(result);
                APMSerializer.GetReponseStatus(obj, rs);
            }

            return Json(rs);
        }

        [HttpPut]
        public JsonResult Activate(string monitorId)
        {
            ResponseStatus rs = new ResponseStatus();
            string query = string.Format("authtoken={0}&monitorid={1}", AuthToken, monitorId);
            string apiurl = GeneralUtility.GetAPIUrl(AppSettings.APIHost, AppSettings.APIPort, "monitor", "activate", query);
            var responseObj = GeneralUtility.SendData(apiurl, null, false, null, MethodType.PUT);
            if (responseObj != null)
            {
                responseObj.Wait();
                var result = responseObj.Result.ToString();
                object obj = JSONParser.ToObject(result);
                APMSerializer.GetReponseStatus(obj, rs);
            }

            return Json(rs);
        }

        [HttpDelete]
        public JsonResult Delete(string monitorId)
        {
            ResponseStatus rs = new ResponseStatus();
            string query = string.Format("authtoken={0}&monitorid={1}", AuthToken, monitorId);
            string apiurl = GeneralUtility.GetAPIUrl(AppSettings.APIHost, AppSettings.APIPort, "monitor", "delete", query);
            var responseObj = GeneralUtility.SendData(apiurl, null, false, null, MethodType.DELETE);
            if (responseObj != null)
            {
                responseObj.Wait();
                var result = responseObj.Result.ToString();
                object obj = JSONParser.ToObject(result);
                APMSerializer.GetReponseStatus(obj, rs);
            }

            return Json(rs);
        }
    }
}
