using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Site24x7.Web.Models;

namespace Site24x7.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly AppSettings _settings;

        protected AppSettings AppSettings
        {
            get
            {
                return _settings;
            }
        }

        public BaseController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }
    }
}

