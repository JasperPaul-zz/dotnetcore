using System.Text;

namespace Site24x7API.Util
{
    public class ResponseParser
    {
        public static string ParseAuthToken(string authTokenResponse)
        {
            string result = null;
            try
            {
                StringBuilder sb = new StringBuilder();
                using (System.IO.StringReader reader = new System.IO.StringReader(authTokenResponse))
                {
                    string line = reader.ReadLine();
                    SplitString(sb, line);
                    while (!string.IsNullOrEmpty(line))
                    {
                        line = reader.ReadLine();
                        SplitString(sb, line);
                    }
                }

                result = string.Format("{{{0}}}", sb.ToString().TrimEnd(','));
            }
            catch
            {
            }

            return result;
        }

        private static void SplitString(StringBuilder sb, string line)
        {
            if (!string.IsNullOrEmpty(line) && !line.StartsWith("#"))
            {
                string[] arr = line.Split('=');
                if (arr.Length == 2)
                {
                    sb.AppendFormat("\"{0}\":\"{1}\",", arr[0], arr[1]);
                }
            }
        }
    }
}
