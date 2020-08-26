using DCXWRUATF.UITests.Support;
using OpenQA.Selenium;
using System;
using System.Text;
using System.Threading;

namespace DCXWRUATF.UITests.Pages
{
    public class BasePage
    {
        private static IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public static void WaitForReady(int timeout = 60)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            var start = DateTime.Now;

            while (!start.HasTimedOut(timeout))
            {
                Thread.Sleep(100);

                if (js.ExecuteScript("return document.readyState;").Equals("complete"))
                {
                    return;
                }
            }
        }

        private static T GetInstance<T>(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        public T Navigate<T>(string url)
        {
            _driver.Url = url;
            _driver.Navigate();
            WaitForReady();
            return GetInstance<T>(_driver);
        }

        public static string CallStack(string error)
        {
            return new StringBuilder()
                .AppendLine(error)
                .AppendLine("Time: " + DateTime.Now.ToString("HH:mm:ss"))
                .AppendLine("Agent: " + Environment.MachineName)
                .ToString();
        }
    }
}
