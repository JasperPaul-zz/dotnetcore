using Site24x7Api.Util;
using Site24x7API.Util;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Site24x7.Api
{
    public class UserAccount
    {
        public string GetAuthToken(string userName, string password)
        {
            string authToken = string.Empty;
            try
            {
                var body = new List<KeyValuePair<string, string>>
                                {
                                    new KeyValuePair<string, string>("SCOPE", "Site24x7/site24x7api"),
                                    new KeyValuePair<string, string>("EMAIL_ID", userName),
                                    new KeyValuePair<string, string>("PASSWORD", password),
                                };
                string actionString = string.Format("{0}/{1}/{2}", "apiauthtoken", "nb", "create");
                var urlBuilder = HttpSendData.GetUrlBuilder(APIType.ZOHO_ACCOUNT, actionString, string.Empty);
                var responseObj = HttpSendData.PostData(urlBuilder.Url, body);

                if (responseObj != null)
                {
                    responseObj.Wait();
                    authToken = ResponseParser.ParseAuthToken(responseObj.Result);
                }
            }
            catch
            {
            }
            return authToken;
        }

        public string GetUser(string authToken)
        {
            string result = string.Empty;
            try
            {
                string query = string.Format("{0}={1}", "authtoken", authToken);
                string actionString = string.Format("{0}/{1}", "api", "users");
                var urlBuilder = HttpSendData.GetUrlBuilder(APIType.SITE24X7_API, actionString, query);
                var responseObj = HttpSendData.SendData(urlBuilder.Url, null, true, null, MethodType.GET);

                if (responseObj != null)
                {
                    responseObj.Wait();
                    result = responseObj.Result.ToString();
                }
                else
                    result = "NULL";
            }
            catch
            {
            }
            return result;
        }
    }
}
