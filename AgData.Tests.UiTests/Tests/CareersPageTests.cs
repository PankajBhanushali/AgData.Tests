using AgData.Tests.UiTests.Pages;
using Framework;
using Framework.Selenium;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace AGData.Tests.UiTests
{
    public class CareersPageTests
    {
        [SetUp]
        public void BeforeEach()
        {
            FW.SetLogger();
            Driver.Init();
            AllPages.Init();
            Driver.Goto(FW.Config.Test.Url);
        }

        /// <summary>
        /// This test will open Careers Page -> Open Job with Specific Keyword
        /// </summary>
        /// <param name="keyword"></param>
        [TestCase("Manager")]
        [Category("Careers")]
        public void Click_On_Second_Link_With_Specific_Keyword(string keyword)
        {
            string jobTitle = AllPages.CareersPage.Goto().OpenJobsByPartialText(keyword, 2);
            Assert.IsTrue(AllPages.CareerDetailsPage.GetJobTitle().Text.Equals(jobTitle));
            FW.Log.Step($"Verified : Careers Page for {jobTitle} Opened As Expected");
        }

        [TearDown]
        public void AfterEach()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;

            if (outcome == TestStatus.Failed)
            {
                Driver.TakeScreenshot("test_failed");
            }

            FW.Log.Info("Outcome: " + outcome);

            Driver.Quit();
        }
    }
}
