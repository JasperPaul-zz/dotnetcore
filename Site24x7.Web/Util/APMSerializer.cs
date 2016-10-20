using Site24x7.Web.Models;
using System;
using System.Collections.Generic;

namespace Site24x7.Web.Util
{
    internal class APMSerializer
    {
        internal static List<User> GetUsers(dynamic jsonObject)
        {
            List<User> users = new List<User>();
            if (jsonObject["code"] != null && jsonObject["code"] == "0")
            {
                foreach (var map in jsonObject.data)
                {
                    User user = new User();
                    user.Email = map["email_address"];
                    user.UserName = Convert.ToString(map["email_address"]).Split('@')[0];
                    users.Add(user);
                }
            }

            return users;
        }

        internal static User GetUser(dynamic jsonObject, string emailid)
        {
            User user = null;
            if (jsonObject["RESULT"] != null)
            {
                if (jsonObject["RESULT"] == "TRUE")
                {
                    user = new User();
                    user.IsValid = true;
                    user.Email = emailid;
                    user.AuthToken = jsonObject["AUTHTOKEN"];
                    user.UserName = emailid.Split('@')[0];
                }
                else
                {
                    user = new User();
                    user.IsValid = false;
                    user.ErrorMessage = jsonObject["CAUSE"];
                }
            }

            return user;
        }

        internal static List<Monitor> GetMonitors(dynamic jsonObject)
        {
            List<Monitor> monitors = new List<Monitor>();
            if (jsonObject["code"] != null && jsonObject["code"] == "0")
            {
                foreach (var map in jsonObject.data)
                {
                    Monitor monitor = new Monitor();
                    monitor.MonitorId = map["monitor_id"];
                    monitor.Status = map["state"];
                    monitor.MonitorName = map["display_name"];
                    monitor.MonitorType = map["type"];
                    monitors.Add(monitor);
                }
            }

            return monitors;
        }

        internal static List<CurrentStatus> GetCurrentStatus(dynamic jsonObject)
        {
            List<CurrentStatus> currentStatusList = new List<CurrentStatus>();
            if (jsonObject["code"] != null && jsonObject["code"] == "0")
            {
                foreach (var map in jsonObject.data["monitors"])
                {
                    CurrentStatus currentStatus = new CurrentStatus();
                    currentStatus.MonitorId = map["monitor_id"];
                    currentStatus.Status = map["status"];
                    currentStatus.MonitorName = map["name"];
                    currentStatus.DownReason = map["down_reason"];
                    DateTime dt = DateTime.MinValue;
                    if(map["last_polled_time"] != null)
                        DateTime.TryParse(map["last_polled_time"].ToString(), out dt);
                    currentStatus.LastPolledTime = dt;
                    currentStatusList.Add(currentStatus);
                }
            }

            return currentStatusList;
        }

        internal static void GetReponseStatus(dynamic jsonObject, ResponseStatus rs)
        {
            try
            {
                rs.StatusCode = jsonObject["code"];
                rs.StatusMessage = jsonObject["message"];
            }
            catch
            {
            }
        }
    }
}
