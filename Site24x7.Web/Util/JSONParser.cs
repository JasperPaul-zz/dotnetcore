using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site24x7.Web.Util
{
    public class JSONParser
    {
        public static object ToObject(string json)
        {
            object obj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            return obj;
        }
    }
}
