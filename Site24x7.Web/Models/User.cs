using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site24x7.Web.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string AuthToken { get; set; }
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }
}
