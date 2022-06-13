using Framework;
using Framework.Selenium;
using OpenQA.Selenium;

namespace AgData.Tests.UiTests.Pages
{
    public class CareerDetailsPage : PageBase
    {
        public readonly CareerDetailsPageMap Map;

        public CareerDetailsPage()
        {
            Map = new CareerDetailsPageMap();
        }

        public Element GetJobTitle()
        {
            Driver.SwitchTo("HBIFRAME");
            FW.Log.Step($"Reading Job Title on Careers Page");
            return Map.JobTitle;
        }
    }

    public class CareerDetailsPageMap
    {
        public Element JobTitle => Driver.FindElement(By.XPath("//p[@class='jobtitle']/span"), "Job Title");
        public Element Apply => Driver.FindElement(By.CssSelector("a[class*='btn-apply']"), "Apply Link");
    }
}
