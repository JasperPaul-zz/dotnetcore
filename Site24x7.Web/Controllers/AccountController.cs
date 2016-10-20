using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Site24x7.Web.Models;
using Site24x7.Web.Util;
using System.Collections.Generic;

namespace Site24x7.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IOptions<AppSettings> settings) : base(settings)
        {
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AuthLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(FormCollection fc)
        {
            int err_code = 0;
            if (Request.Form != null)
            {
                string email = string.Empty;
                string password = string.Empty;
                if (!string.IsNullOrEmpty(Request.Form["email"]))
                    email = Request.Form["email"].ToString();
                else
                    err_code = 1;

                if (!string.IsNullOrEmpty(Request.Form["password"]))
                    password = Request.Form["password"].ToString();
                else
                    err_code = 2;

                string apiurl = GeneralUtility.GetAPIUrl(AppSettings.APIHost, AppSettings.APIPort, "account", "getauthtoken", string.Empty);
                var body = new List<KeyValuePair<string, string>>
                                {
                                    new KeyValuePair<string, string>("EMAIL_ID", email),
                                    new KeyValuePair<string, string>("PASSWORD", password),
                                };

                var responseObj = GeneralUtility.PostData(apiurl, body);
                if (responseObj != null)
                {
                    responseObj.Wait();
                    var result = responseObj.Result.ToString();
                    object obj = JSONParser.ToObject(result);
                    User user = APMSerializer.GetUser(obj, email);
                    if (user != null)
                    {
                        if (user.IsValid)
                        {
                            HttpContext.Session.SetString("email", user.Email);
                            HttpContext.Session.SetString("username", user.UserName);
                            HttpContext.Session.SetString("authtoken", user.AuthToken);
                            return RedirectToAction("ListMonitors", "Monitor");
                        }
                        else
                        {
                            err_code = 11;
                            return RedirectToAction("Login", "Account", new { e = err_code, mesg = user.ErrorMessage });
                        }
                    }
                }
            }

            return RedirectToAction("Login", "Account", new { e = err_code });
        }

        [HttpPost]
        public IActionResult AuthLogin(FormCollection fc)
        {
            if (Request.Form != null && !string.IsNullOrEmpty(Request.Form["authtoken"]))
            {
                string query = string.Format("{0}={1}", "authtoken", Request.Form["authtoken"]);
                string apiurl = GeneralUtility.GetAPIUrl(AppSettings.APIHost, AppSettings.APIPort, "account", "getuser", query);
               
                var responseObj = GeneralUtility.SendData(apiurl, null, false, null, MethodType.GET);
                if (responseObj != null)
                {
                    responseObj.Wait();
                    var result = responseObj.Result.ToString();
                    object obj = JSONParser.ToObject(result);
                    List<User> users = APMSerializer.GetUsers(obj);
                    if (users.Count>0)
                    {
                            HttpContext.Session.SetString("email", users[0].Email);
                            HttpContext.Session.SetString("username", users[0].UserName);
                            HttpContext.Session.SetString("authtoken", Request.Form["authtoken"]);
                            return RedirectToAction("ListMonitors", "Monitor");
                    }
                }
            }

            return RedirectToAction("Login", "Account", new { e = 222 });
        }
    }
}
