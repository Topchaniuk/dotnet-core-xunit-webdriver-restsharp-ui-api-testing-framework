using OpenQA.Selenium;

namespace DCXWRUATF.UITests.Controls
{
    public class Button : BaseElement
    {
        public Button(IWebDriver driver, By by, int timeout = 3) : base(driver, by, timeout) { }
        public Button(IWebDriver driver, string xpath, int timeout = 3) : base(driver, xpath, timeout) { }
        public Button(IWebDriver driver, BaseElement element) : base(driver, element) { }

    }
}
