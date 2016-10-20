using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Site24x7.Web.Util
{
    public enum MethodType
    {
        POST,
        GET,
        PUT,
        DELETE
    }

    public class GeneralUtility
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
                    request.ContentType = "application/json";
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
                    var response = client.PostAsync(url, content);
                    response.Wait();
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

        public static string GetAPIUrl(string host, int port, string controller, string action, string queryString)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("http://").Append(host).Append(":").Append(port).Append("/");
            sb.Append("api").Append("/");
            sb.Append(controller).Append("/").Append(action);
            if(!string.IsNullOrEmpty(queryString))
            {
                sb.Append("?").Append(queryString);
            }
            return sb.ToString();
        }
    }
}
