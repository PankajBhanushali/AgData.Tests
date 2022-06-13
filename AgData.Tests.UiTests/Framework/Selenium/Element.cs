using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Framework.Selenium
{
    public class Element : IWebElement
    {
        private readonly IWebElement _element;

        public readonly string Name;

        public By FoundBy { get; set; }

        public Element(IWebElement element, string name)
        {
            _element = element;
            Name = name;
        }

        public IWebElement Current => _element ?? throw new NullReferenceException("Current IWebElement _element is null.");

        public string TagName => Current.TagName;

        public string Text => Current.Text;

        public bool Enabled => Current.Enabled;

        public bool Selected => Current.Selected;

        public Point Location => Current.Location;

        public Size Size => Current.Size;

        public bool Displayed => Current.Displayed;

        public void Clear()
        {
            Current.Clear();
        }

        public void Click()
        {
            FW.Log.Step($"Click {Name}");
            Current.Click();
        }

        public void JSClick()
        {
            FW.Log.Step($"JS Click {Name}");
            ((IJavaScriptExecutor)Driver.Current).ExecuteScript("arguments[0].click();", Current);
        }

        public void Hover()
        {
            var actions = new Actions(Driver.Current);
            try
            {
                actions.MoveToElement(Current).Perform();
            }
            catch (ElementClickInterceptedException ex)
            {
                FW.Log.Step($"Unable to Hover Over {this.Name}");
                throw ex;
            }
        }

        public void Highlight()
        {
            ((IJavaScriptExecutor)Driver.Current).ExecuteScript("arguments[0].style.border='3px dotted blue'", Current);
        }

        public void ScrollToView()
        {
            if (Current.Location.Y > 200)
            {
                ScrollTo(0, Current.Location.Y - 100); // Make sure element is in the view but below the top navigation pane
            }

        }
        public void ScrollTo(int xPosition = 0, int yPosition = 0)
        {
            var js = String.Format("window.scrollTo({0}, {1})", xPosition, yPosition);
            ((IJavaScriptExecutor)Driver.Current).ExecuteScript(js);
        }

        public IWebElement FindElement(By by)
        {
            return Current.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return Current.FindElements(by);
        }

        public string GetAttribute(string attributeName)
        {
            return Current.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return Current.GetCssValue(propertyName);
        }

        [Obsolete("This method is obsoleted in Selenium 4")]
        public string GetProperty(string propertyName)
        {
            return Current.GetProperty(propertyName);
        }
        public string GetDomAttribute(string attributeName)
        {
            return Current.GetDomAttribute(attributeName);
            throw new NotImplementedException();
        }

        public string GetDomProperty(string propertyName)
        {
            return Current.GetDomProperty(propertyName);
        }

        public ISearchContext GetShadowRoot()
        {
            return Current.GetShadowRoot();
        }

        public void SendKeys(string text)
        {
            Current.SendKeys(text);
        }

        public void Submit()
        {
            Current.Submit();
        }
    }
}
