using System.Text;

namespace Site24x7Api.Util
{
    internal enum APIType
    {
        ZOHO_ACCOUNT,
        SITE24X7_API
    }

    internal class UrlBuilder
    {
        public UrlBuilder(string protocol, string host, int port, string queryString)
        {
            StringBuilder url = new StringBuilder();
            url.Append(protocol).Append("://").Append(host).Append(":").Append(port.ToString()).Append(queryString);
            Url = url.ToString();
        }

        public UrlBuilder(string protocol, string domain, string queryString)
        {
            StringBuilder url = new StringBuilder();
            url.Append(protocol).Append("://").Append(domain);

            if(!string.IsNullOrEmpty(queryString))
                url.Append("?").Append(queryString);

            Url = url.ToString();
        }

        public string Url
        {
            get;
            private set;
        }
    }
}
