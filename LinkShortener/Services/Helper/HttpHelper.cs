using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LinkShortener.Services.Helper
{
    public static class HttpHelper
    {
        /// <summary>
        /// get url and return html response
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> HttpGet(string url)
        {
            string responseContent = null;
            try
            {
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    responseContent = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                //ignore
            }
            return responseContent;
        }
    }
}
