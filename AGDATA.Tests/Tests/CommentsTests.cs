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

namespace AGDATA.Tests
{
    [Parallelizable(scope: ParallelScope.All)]
    public class CommentsTests
    {
        private CommentsServiceClient _commentsClient;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _commentsClient = new CommentsServiceClient();
        }

        [SetUp]
        public void Startup()
        {
            TestContext.WriteLine($"Starting Test Execution : Test Name : {TestContext.CurrentContext.Test.Name}");
        }

        /// <summary>
        /// Test verifies listing all Comments by Post ID
        /// </summary>
        [TestCase("3", HttpStatusCode.OK, TestName = "Get Comments by valid Post ID should return OK")]
        [TestCase("-1", HttpStatusCode.OK, TestName = "Get Comments by negative Post ID should return OK")]
        [TestCase("alphanumeric123", HttpStatusCode.OK, TestName = "Get Comments by alphanumeric Post ID should return OK")]
        public void Get_Comments_By_PostId_Should_Return_Ok(string postId, HttpStatusCode expectedStatusCode)
        {
            var response = _commentsClient.GetAsync(postId);
            response.Result.StatusCode.Should().Be(expectedStatusCode);

            var jsonOutput = JsonConvert.DeserializeObject<List<CommentsOutputModel>>(response.Result.Content.ReadAsStringAsync().Result.ToString());
            if (jsonOutput.FirstOrDefault() != null)
                jsonOutput.Any(x => x.postId != int.Parse(postId)).Should().BeFalse();
        }

        [TearDown]
        public void TearDown()
        {
            TestContext.WriteLine($"Test Execution Completed : Test Name : {TestContext.CurrentContext.Test.Name} Result : {TestContext.CurrentContext.Result.Outcome}");
        }
    }
}