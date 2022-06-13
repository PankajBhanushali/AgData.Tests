using AGDATA.ApiTests.Helper;
using AGDATA.ApiTests.Models;
using AGDATA.ApiTests.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Serilog;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AGDATA.ApiTests.ServiceClients
{
    class PostsServiceClient
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly static string PostsUrlPath = "posts";

        public static Uri PostsEndpoint() => UrlBuilder.BuildUrl(Configuration.BaseUrl, PostsUrlPath);
        public static Uri PostsEndpoint(int id) => UrlBuilder.BuildUrl(Configuration.BaseUrl, PostsUrlPath, id.ToString());

        internal async Task<HttpResponseMessage> GetAsync()
        {
            var response = await httpClient.GetAsync(PostsEndpoint());
            if (response.IsSuccessStatusCode)
                TestContext.WriteLine($"Successfully fetched all Posts");
            else
                Log.Debug($"Failed to fetch Posts. StatusCode : {response.StatusCode} Reason: {response.Content.ReadAsStringAsync().Result}");
            return response;
        }

        internal async Task<HttpResponseMessage> GetAsync(int userId)
        {
            var response = await httpClient.GetAsync(PostsEndpoint() + $"?userId={userId}");
            if (response.IsSuccessStatusCode)
                TestContext.WriteLine($"Successfully fetched all Posts");
            else
                Log.Debug($"Failed to fetch Posts. StatusCode : {response.StatusCode} Reason: {response.Content.ReadAsStringAsync().Result}");
            return response;
        }

        internal async Task<HttpResponseMessage> PostAsync(PostsInputModel pim)
        {
            var body = new StringContent(JsonConvert.SerializeObject(pim), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(PostsEndpoint(), body);
            if (response.IsSuccessStatusCode)
                TestContext.WriteLine($"Successfully Created Post : With ID : {pim.userId}");
            else
                Log.Debug($"Failed to create Post. StatusCode : {response.StatusCode} Reason: {response.Content.ReadAsStringAsync().Result}");
            return response;
        }

        internal async Task<HttpResponseMessage> PutAsync(PostsOutputModel pom)
        {
            var body = new StringContent(JsonConvert.SerializeObject(pom), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(PostsEndpoint(pom.id), body);
            if (response.IsSuccessStatusCode)
                TestContext.WriteLine($"Successfully Updated Post : With ID : {pom.id}");
            else
                Log.Debug($"Failed to update Post. StatusCode : {response.StatusCode} Reason: {response.Content.ReadAsStringAsync().Result}");
            return response;
        }

        internal async Task<HttpResponseMessage> PatchAsync(int id, JObject bodyObject)
        {
            var bodyContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync(PostsEndpoint(id), bodyContent);
            if (response.IsSuccessStatusCode)
                TestContext.WriteLine($"Successfully Patched Post : With ID : {id}");
            else
                Log.Debug($"Failed to Patch Post. StatusCode : {response.StatusCode} Reason: {response.Content.ReadAsStringAsync().Result}");
            return response;
        }

        internal async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var response = await httpClient.DeleteAsync(PostsEndpoint(id));
            if (response.IsSuccessStatusCode)
                TestContext.WriteLine($"Successfully Deleted Post : With ID : {id}");
            else
                Log.Debug($"Failed to Delete Post. StatusCode : {response.StatusCode} Reason: {response.Content.ReadAsStringAsync().Result}");
            return response;
        }

        internal PostsInputModel GenerateRandomPostInput()
        {
            PostsInputModel pim = new PostsInputModel();
            pim.userId = new Random().Next(int.MaxValue);
            pim.title = "title" + pim.userId;
            pim.body = "body" + pim.body;

            return pim;
        }

        internal PostsOutputModel GenerateRandomPutInputModel()
        {
            PostsOutputModel pom = new PostsOutputModel();
            pom.id = new Random().Next(100);
            pom.userId = new Random().Next(int.MaxValue);
            pom.title = "title" + pom.userId;
            pom.body = "body" + pom.body;

            return pom;
        }
    }
}
