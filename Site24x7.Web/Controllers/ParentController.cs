using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Site24x7.Web.Models;

namespace Site24x7.Web.Controllers
{
    public class ParentController : BaseController
    {
        public string AuthToken { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }

        private bool isSessionSet = false;

        public ParentController(IOptions<AppSettings> settings) : base(settings)
        {
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!isSessionSet)
            {
                ControllerContext.HttpContext.Response.Redirect("../Account/Login");
            }
            else
            {
                ViewBag.UserName = UserName;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            InitializeContext();
        }

        private void InitializeContext()
        {
            if (HttpContext.Session != null)
            {
                if (HttpContext.Session.GetString("authtoken") != null)
                {
                    AuthToken = HttpContext.Session.GetString("authtoken").ToString();
                    Email = HttpContext.Session.GetString("email").ToString();
                    UserName = HttpContext.Session.GetString("username").ToString();
                    isSessionSet = true;
                }
                else
                    isSessionSet = false;
            }
        }

        protected void SetAuthToken(string authtoken)
        {
            HttpContext.Session.SetString("authtoken", authtoken);
        }

        protected void ClearSession()
        {
            HttpContext.Session.Clear();
        }
    }
}
