using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Services.LinkShortener.Services.Main
{
    public class HttpManagementService : IHttpManagementService
    {
        #region Fields



        #endregion
        #region Methods
        public async Task<string> RequestAsync(string url, string jsonContent, HttpMethod HttpMethod, Dictionary<string, string> headers = null, bool XRequestedWithHeader = false,
            string requestHeaderType = "application/json")
        {
            string responseString = null;
            var client = new HttpClient();
            HttpResponseMessage response;
            //get url
            if (HttpMethod == HttpMethod.Get)
            {
                //get query string
                var queryString = ConvertJsonContentToQueryString(jsonContent);
                if (!string.IsNullOrEmpty(queryString))
                {
                    //set query string
                    url = url + "?" + queryString;
                }

                response = await client.GetAsync(url);
            }
            //post url
            else if (HttpMethod == HttpMethod.Post)
            {
                if (requestHeaderType.ToLower() == "application/x-www-form-urlencoded")
                {
                    jsonContent = ConvertJsonContentToQueryString(jsonContent);
                }
                //convert json to bytes
                var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                //buffer bytes
                var byteContent = new ByteArrayContent(buffer);
                //set header content
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(requestHeaderType);
                if (headers != null)
                    foreach (var header in headers)
                    {
                        byteContent.Headers.Add(header.Key, header.Value);
                    }

                //headers
                if (XRequestedWithHeader)
                    byteContent.Headers.Add("X-Requested-With", "XMLHttpRequest");
                //post url and content
                response = await client.PostAsync(url, byteContent);
            }
            else
            {
                throw new Exception("Invalid Request Method");
            }

            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();
            }

            return responseString;
        }

        public async Task<HttpStatusCode> RequestStatusCodeAsync(string url, string jsonContent, HttpMethod HttpMethod,
            string requestHeaderType = "application/json")
        {
            var client = new HttpClient();
            HttpResponseMessage response;
            //get url
            if (HttpMethod == HttpMethod.Get)
            {
                //get query string
                var queryString = ConvertJsonContentToQueryString(jsonContent);
                if (!string.IsNullOrEmpty(queryString))
                {
                    //set query string
                    url = url + "?" + queryString;
                }

                response = await client.GetAsync(url);
            }
            //post url
            else if (HttpMethod == HttpMethod.Post)
            {
                if (requestHeaderType.ToLower() == "application/x-www-form-urlencoded")
                {
                    jsonContent = ConvertJsonContentToQueryString(jsonContent);
                }
                //convert json to bytes
                var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                //buffer bytes
                var byteContent = new ByteArrayContent(buffer);
                //set header content
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(requestHeaderType);
                //post url and content
                response = await client.PostAsync(url, byteContent);
            }
            else
            {
                throw new Exception("Invalid Request Method");
            }

            return response.StatusCode;
        }

        public string ConvertJsonContentToQueryString(string jsonContent)
        {
            if (string.IsNullOrEmpty(jsonContent))
                return "";
            var jObj = (JObject)JsonConvert.DeserializeObject(jsonContent);

            var query = String.Join("&",
                jObj.Children().Cast<JProperty>()
                    .Select(jp => jp.Name + "=" + HttpUtility.UrlEncode(jp.Value.ToString())));

            //var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            //StringBuilder sb = new StringBuilder();
            //foreach (KeyValuePair<string, string> kvp in dict)
            //{
            //    if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
            //    {
            //        if (sb.Length > 0) sb.Append('&');
            //        sb.Append(HttpUtility.UrlEncode(kvp.Key));
            //        sb.Append('=');
            //        sb.Append(HttpUtility.UrlEncode(kvp.Value));
            //    }
            //}

            return query;
        }

        #endregion
        #region Utilities



        #endregion
        #region Ctor



        #endregion


    }
}