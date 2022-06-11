using AGDATA.ApiTests.Helper;
using AGDATA.ApiTests.Settings;
using NUnit.Framework;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AGDATA.ApiTests.ServiceClients
{
    class CommentsServiceClient
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly static string PostsUrlPath = "comments";

        public static Uri CommentsEndpoint() => UrlBuilder.BuildUrl(Configuration.BaseUrl, PostsUrlPath);

        internal async Task<HttpResponseMessage> GetAsync(string postId)
        {
            var response = await httpClient.GetAsync(CommentsEndpoint() + $"?postId={postId}");
            if (response.IsSuccessStatusCode)
                TestContext.WriteLine($"Successfully fetched all Posts");
            else
                Log.Debug($"Failed to fetch Posts. StatusCode : {response.StatusCode} Reason: {response.Content.ReadAsStringAsync().Result}");
            return response;
        }
    }
}
