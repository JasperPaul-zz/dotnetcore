using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Site24x7Api.Util
{
    internal enum MethodType
    {
        POST,
        GET,
        PUT,
        DELETE
    }

    internal class HttpSendData
    {
        public static async Task<object> SendData(string url, object data, bool isHttps, IWebProxy wp, MethodType methodType = MethodType.POST)
        {
            object responseObj = null;
            WebResponse response = null;
            Stream dataStream = null;
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                if (wp != null)
                {
                    request.Proxy = wp;
                }

                request.Method = methodType.ToString();
                if (data != null)
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(data.ToString());
                    request.ContentType = "form-data";
                    dataStream = await request.GetRequestStreamAsync();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Flush();
                }

                DateTime startTime = DateTime.Now;
                response = await request.GetResponseAsync();
                DateTime endtime = DateTime.Now;

                if ((response as HttpWebResponse).StatusCode == HttpStatusCode.OK)
                {
                    dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    responseObj = responseFromServer;
                    reader.Dispose();
                }

            }
            catch (WebException we)
            {
                response = we.Response;
                if (response != null)
                {
                    dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseException = reader.ReadLine();
                    responseObj = responseException;
                }
            }
            finally
            {
                if (dataStream != null)
                    dataStream.Dispose();
                if (response != null)
                    response.Dispose();
            }
            return responseObj;
        }

        public static Task<string> PostData(string url, List<KeyValuePair<string, string>> body, string contentType = @"application/x-www-form-urlencoded")
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(body);
                    content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
                    DateTime startTime = DateTime.Now;
                    var response = client.PostAsync(url, content);
                    response.Wait();
                    DateTime endTime = DateTime.Now;
                    if (response.IsCompleted)
                    {
                        return response.Result.Content.ReadAsStringAsync();
                    }
                }
            }
            catch
            {
            }
            return null;
        }

        public static UrlBuilder GetUrlBuilder(APIType apiType, string actionString, string queryString)
        {
            StringBuilder url = new StringBuilder();
            string domain = GetDomain(apiType);
            url.Append(domain);
            url.Append("/");
            url.Append(actionString);
            string protocol = "https";
            UrlBuilder urlBuilder = new UrlBuilder(protocol, url.ToString(), queryString);
            return urlBuilder;
        }

        //public static string GetQueryString(Hashtable queryStringMap)
        //{
        //    StringBuilder queryString = new StringBuilder();
        //    if (queryStringMap != null)
        //    {
        //        foreach (var key in queryStringMap.Keys)
        //        {
        //            queryString.Append(key).Append("=").Append(queryStringMap[key]);
        //        }

        //        if (queryStringMap.Count > 0)
        //        {
        //            queryString.Insert(0, "?");
        //            queryString.Remove(queryString.Length - 1, 1);
        //        }
        //    }
        //    return queryString.ToString();
        //}

        private static string GetDomain(APIType apiType)
        {
            string domain = string.Empty;
            switch (apiType)
            {
                case APIType.ZOHO_ACCOUNT:
                    domain = "accounts.zoho.com";
                    break;
                case APIType.SITE24X7_API:
                    domain = "www.site24x7.com";
                    break;
            }
            return domain;
        }
    }
}
