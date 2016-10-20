using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Site24x7.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        [HttpPost]
        public string GetAuthToken()
        {
            string email = string.Empty;
            string password = string.Empty;
            if (Request.Form != null)
            {
                if (!string.IsNullOrEmpty(Request.Form["EMAIL_ID"]))
                    email = Request.Form["EMAIL_ID"].ToString();
                if (!string.IsNullOrEmpty(Request.Form["PASSWORD"]))
                    password = Request.Form["PASSWORD"].ToString();
            }
            Site24x7.Api.UserAccount userAcc = new Site24x7.Api.UserAccount();
            return userAcc.GetAuthToken(email, password);
        }

        [HttpGet]
        public string GetUser(string authToken)
        {
            Site24x7.Api.UserAccount userAcc = new Site24x7.Api.UserAccount();
            return userAcc.GetUser(authToken);
        }
    }
}
