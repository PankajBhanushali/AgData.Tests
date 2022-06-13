using AGDATA.ApiTests.Models;
using AGDATA.ApiTests.ServiceClients;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AGDATA.ApiTests.Tests
{
    [Parallelizable(scope: ParallelScope.All)]
    public class PostTests
    {
        private PostsServiceClient _postClient;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _postClient = new PostsServiceClient();
        }

        [SetUp]
        public void Startup()
        {
            TestContext.WriteLine($"Starting Test Execution : Test Name : {TestContext.CurrentContext.Test.Name}");
        }

        /// <summary>
        /// Test verifies listing all resources functionality
        /// </summary>
        [Test]
        public void Get_Posts_Should_Return_Ok()
        {
            var response = _postClient.GetAsync();
            response.Result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Test verifies listing all posts by User ID
        /// </summary>
        [TestCase(100, HttpStatusCode.OK, TestName = "Get Post with valid User ID should return OK")]
        [TestCase(-1, HttpStatusCode.OK, TestName = "Get Post with Negative User ID should return OK")]
        public void Get_Posts_By_UserID_Should_Return_Ok(int userId, HttpStatusCode expectedStatusCode)
        {
            var response = _postClient.GetAsync(userId);
            response.Result.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonOutput = JsonConvert.DeserializeObject<List<PostsOutputModel>>(response.Result.Content.ReadAsStringAsync().Result.ToString());
            if (jsonOutput.Count > 0)
                jsonOutput.Any(x => x.userId != userId).Should().BeFalse();
        }

        /// <summary>
        /// Test verified creating a resource functionality
        /// </summary>
        [Test]
        public void Post_Posts_Should_Return_Created()
        {
            var postData = _postClient.GenerateRandomPostInput();
            var response = _postClient.PostAsync(postData);
            response.Result.StatusCode.Should().Be(HttpStatusCode.Created);

            var jsonOutput = JsonConvert.DeserializeObject<PostsOutputModel>(response.Result.Content.ReadAsStringAsync().Result.ToString());
            (jsonOutput.userId == postData.userId).Should().BeTrue();
            (jsonOutput.body == postData.body).Should().BeTrue();
            (jsonOutput.title == postData.title).Should().BeTrue();

            TestContext.WriteLine($"Verified that new post is created succesfully");
        }

        /// <summary>
        /// Test verifies Updating a resource functionality
        /// </summary>
        [Test]
        public void Update_Posts_Should_Return_Ok()
        {
            var postData = _postClient.GenerateRandomPutInputModel();
            var response = _postClient.PutAsync(postData);
            response.Result.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonOutput = JsonConvert.DeserializeObject<PostsOutputModel>(response.Result.Content.ReadAsStringAsync().Result.ToString());
            (jsonOutput.id == postData.id).Should().BeTrue();
            (jsonOutput.userId == postData.userId).Should().BeTrue();
            (jsonOutput.body == postData.body).Should().BeTrue();
            (jsonOutput.title == postData.title).Should().BeTrue();

            TestContext.WriteLine($"Verified that post is updated succesfully");
        }

        /// <summary>
        /// Test verifies Patching a resource functionality
        /// </summary>
        [Test]
        public void Patch_Posts_Should_Return_Ok()
        {
            //arrange
            JObject patchDynamicInput = JObject.FromObject(new
            {
                title = "new title",
                anotherTitle = "another title object",
            });

            int id = new Random().Next(100);

            //act
            var response = _postClient.PatchAsync(id, patchDynamicInput);

            //assert
            response.Result.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonOutput = JsonConvert.DeserializeObject<JObject>(response.Result.Content.ReadAsStringAsync().Result.ToString());
            jsonOutput.GetValue("id").ToString().Should().BeEquivalentTo(id.ToString());
            jsonOutput.GetValue("title").Should().BeEquivalentTo(patchDynamicInput.GetValue("title"));
            jsonOutput.GetValue("anotherTitle").Should().BeEquivalentTo(patchDynamicInput.GetValue("anotherTitle"));

            TestContext.WriteLine($"Verified that patch is updated succesfully");
        }

        /// <summary>
        /// Test verified Deleting a resource functionality
        /// </summary>
        [TestCase(100, HttpStatusCode.OK, TestName = "Delete Post with Valid Post ID should return OK")]
        [TestCase(-1, HttpStatusCode.OK, TestName = "Delete Post with Negative Post ID should return OK")]
        public void Delete_Posts_Should_Return_Ok(int postId, HttpStatusCode expectedStatusCode)
        {
            var response = _postClient.DeleteAsync(postId);
            response.Result.StatusCode.Should().Be(expectedStatusCode);
        }

        [TearDown]
        public void TearDown()
        {
            TestContext.WriteLine($"Test Execution Completed : Test Name : {TestContext.CurrentContext.Test.Name} Result : {TestContext.CurrentContext.Result.Outcome}");
        }
    }
}