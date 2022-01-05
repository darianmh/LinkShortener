using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LinkShortener.Services.Main
{
    public interface IHttpManagementService
    {
        Task<string> RequestAsync(string url, string jsonContent, HttpMethod httpVerbs, Dictionary<string, string> headers = null,
            bool XRequestedWithHeader = false, string requestHeaderType = "application/json");
        Task<HttpStatusCode> RequestStatusCodeAsync(string url, string jsonContent, HttpMethod httpVerbs, string requestHeaderType = "application/json");
        string ConvertJsonContentToQueryString(string jsonContent);
    }
}