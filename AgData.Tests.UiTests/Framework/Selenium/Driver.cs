using OpenQA.Selenium;
using System;
using System.IO;

namespace Framework.Selenium
{
    public class Driver
    {
        [ThreadStatic]
        static IWebDriver _driver;

        [ThreadStatic]
        public static Wait Wait;

        [ThreadStatic]
        public static WindowManager Window;

        public static void Init(int waitSeconds = 10)
        {
            _driver = DriverFactory.Build(FW.Config.Driver.Browser);
            Wait = new Wait(waitSeconds);
            Window = new WindowManager();
            Window.Maximize();
        }

        public static IWebDriver Current => _driver ?? throw new NullReferenceException("_driver is null. Call Driver.Init() first.");

        public static string Title => Current.Title;

        //By default TimeOut is 60 seconds
        public static void Goto(string url)
        {
            if (!url.StartsWith("https"))
            {
                url = $"https://{url}";
            }

            FW.Log.Step($"Navigate to {url}");
            Current.Navigate().GoToUrl(url);
        }

        public static Element FindElement(By by, string elementName)
        {
            return new Element(Current.FindElement(by), elementName)
            {
                FoundBy = by
            };
        }

        public static Elements FindElements(By by)
        {
            return new Elements(Current.FindElements(by))
            {
                FoundBy = by
            };
        }

        public static void SwitchTo(string frameName)
        {
            Current.SwitchTo().Frame(frameName);
        }

        public static void SwitchTo(int frameIndex)
        {
            Current.SwitchTo().Frame(frameIndex);
        }

        public static void SwitchToParent()
        {
            Current.SwitchTo().ParentFrame();
        }

        public static void SwitchToDefaultContent()
        {
            Current.SwitchTo().DefaultContent();
        }

        public static void TakeScreenshot(string imageName)
        {
            var ss = ((ITakesScreenshot)Current).GetScreenshot();
            var ssFileName = Path.Combine(FW.CurrentTestDirectory.FullName, imageName);
            ss.SaveAsFile($"{ssFileName}.png", ScreenshotImageFormat.Png);
        }

        public static void Quit()
        {
            FW.Log.Info("Close browser");
            Current.Quit();
            Current.Dispose();
        }
    }
}
