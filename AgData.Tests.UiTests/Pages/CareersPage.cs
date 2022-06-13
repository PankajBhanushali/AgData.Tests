using Framework;
using Framework.Selenium;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;

namespace AgData.Tests.UiTests.Pages
{
    public class CareersPage : PageBase
    {
        public readonly CareersPageMap Map;

        public CareersPage()
        {
            Map = new CareersPageMap();
        }

        public CareersPage Goto()
        {
            HeaderNav.GotoCareersPage();
            return this;
        }

        public Element GetJob(string jobName)
        {
            Driver.SwitchTo("HBIFRAME");
            var jobs = Map.GetJob(jobName);
            return jobs;
        }

        public string OpenJobsByPartialText(string partialText, int elementIndex)
        {
            Driver.SwitchTo("HBIFRAME");
            var jobs = Map.GetJobsByPartialText(partialText);
            var openJob = jobs.AsQueryable().ElementAt(elementIndex - 1);
            var ele = new Element(Driver.Wait.Until(WaitConditions.ElementIsDisplayed(openJob)), $"Job With Partial Text : {partialText}");
            string jobTitle = ele.Text;
            
            //ele.ScrollToView(); Not working
            //ele.Hover(); Not working
            //ele.Click();//Not working
            ele.JSClick(); //Using JavaScriptExecutor - Click to click on Job

            Driver.SwitchToDefaultContent();
            return jobTitle;
        }

        public Elements GetAllJobs()
        {
            Driver.SwitchTo("HBIFRAME");
            var jobs = Map.GetAllJobs();
            foreach (var item in jobs)
            {
                FW.Log.Step($"Job Text : " + item.Text);
            }
            Driver.SwitchToParent();
            return jobs;
        }

        public CareersPage ClickOnJob(Element job)
        {
            //Driver.SwitchTo("HBIFRAME");
            job.Click();
            return this;
        }

        public CareersPage ClickOnJob(IWebElement job)
        {
            Driver.SwitchTo("HBIFRAME");
            job.Click();
            return this;
        }
    }

    public class CareersPageMap
    {
        public Element OpenPositionsLabel() => Driver.FindElement(By.XPath($"//span[contains(text(),'Open Positions')]"), "Open Positions");

        public Element GetJob(string jobName) => Driver.FindElement(By.XPath($"//a[contains(text(),'{jobName}')]"), "Job");

        public Elements GetJobsByPartialText(string partialText) => Driver.FindElements(By.XPath($"//a[contains(text(),'{partialText}')]"));

        public Elements GetAllJobs() => Driver.FindElements(By.XPath("//ul/li/span[@class='job']/a"));
    }
}
