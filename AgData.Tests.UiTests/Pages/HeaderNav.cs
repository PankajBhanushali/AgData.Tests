using Framework.Selenium;
using OpenQA.Selenium;

namespace AgData.Tests.UiTests.Pages
{
    public class NavigationBar
    {
        public readonly NavigationBarMap Map;

        public NavigationBar()
        {
            Map = new NavigationBarMap();
        }

        public void GotoCareersPage()
        {
            Map.CompanyLink.Hover();
            Map.CareersLink.Click();
        }

        public void GotoPage(string parentLink, string childLink)
        {
            Map.Link(parentLink).Hover();
            Map.Link(childLink).Click();
        }
    }

    public class NavigationBarMap
    {
        public Element Link(string linkText) => Driver.FindElement(By.XPath($"//a[contains(text(),'{linkText}')]"), $"{linkText} Link");
        public Element CompanyLink => Driver.FindElement(By.XPath("//a[contains(text(),'Company')]"), "Company Link");
        public Element CareersLink => Driver.FindElement(By.XPath("//a[contains(text(),'Careers')]"), "Careers Link");
    }
}
