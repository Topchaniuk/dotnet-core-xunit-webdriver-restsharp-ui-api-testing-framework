using DCXWRUATF.UITests.Pages;
using DCXWRUATF.UITests.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DCXWRUATF.UITests.Controls
{
    public class BaseElement
    {
		private readonly int _timeout;
		private readonly By _by;
		private readonly IWebElement _control;
		private readonly IWebDriver _driver;

		protected string XPath => GetXPath(_by);

		public BaseElement(IWebDriver driver, By by, int timeout = 3)
		{
			_driver = driver;
			_by = by;
			_timeout = timeout;
		}

		public BaseElement(IWebDriver driver, string xpath, int timeout = 3)
		{
			_driver = driver;
			_by = By.XPath(xpath);
			_timeout = timeout;
		}

		protected internal BaseElement(IWebDriver driver, BaseElement element)
		{
			_driver = driver;
			_control = element?._control;
			_by = element?._by;
		}

		private BaseElement(IWebDriver driver, IWebElement element, By by)
		{
			_driver = driver;
			_control = element;
			_by = by;
		}

		protected string GetXPath(By by)
		{
			var locator = by.ToString();
			var search = locator.SubString(":").Trim();

			if (locator.StartsWith("By.Id"))
			{
				return $"//*[@id='{search}']";
			}
			if (locator.StartsWith("By.ClassName"))
			{
				return $"//*[@class='{search}']";
			}
			if (locator.StartsWith("By.LinkText"))
			{
				return $"//a[text()='{search}']";
			}
			if (locator.StartsWith("By.XPath"))
			{
				return search;
			}

			throw new NotImplementedException($"'{locator}' - not covered");
		}

		protected internal IWebElement Control
		{
			get
			{
				if (_control != null) return _control;

				try
				{
					new WebDriverWait(_driver, TimeSpan.FromSeconds(_timeout)).Until(driver => driver.FindElements(_by).Count > 0);
					return _driver.FindElement(_by);
				}
				catch
				{
					CallStack($"Not found in {_timeout} seconds!");
				}

				return null;
			}
		}

		public string Text
		{
			get
			{
				if (!Exists) return null;

				var text = Control.Text.Trim();
				var value = GetAttribute("value");
				var textContent = GetAttribute("textContent");

				if (text != string.Empty) return text;
				if (value != string.Empty) return value;

				return textContent;
			}
		}

		public bool HasAttribute(string attribute)
		{
			return !GetAttribute(attribute).Equals(string.Empty);
		}

		public string GetAttribute(string attribute)
		{
			try
			{
				return Control.GetAttribute(attribute).Trim();
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		public string GetAttributeValue(string attribute)
		{
			IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
			const string script = "return arguments[0].getAttribute(arguments[1]) || '';";
			return (string)js.ExecuteScript(script, Control, attribute);
		}

		public void Clear()
		{
			Click();
			Control.SendKeys(Keys.LeftControl + "a");
			Control.SendKeys(Keys.Delete);
			BasePage.WaitForReady();
		}

		public void SendKeys(string text)
		{
			foreach (var symbol in text)
			{
				Control.SendKeys(symbol.ToString());
			}
		}

		public void SetText(int? value)
		{
			if (value != null)
			{
				SetText(value.Value);
			}
		}

		protected void SetText(int value)
		{
			SetText(value.ToString());
		}

		protected void SetText(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				Clear();
				return;
			}

			Click();
			Control.SendKeys(Keys.LeftControl + "a");
			SendKeys(text);
		}

		public void SetTextInARow(string text)
		{
			Control.SendKeys(text);
		}

		public void SetText(DateTime date)
		{
			SetText(date.ToString("dd-MM-yyyy"));
		}

		public void SetText(DateTime? date)
		{
			if (date != null)
			{
				SetText(date.Value.ToString("dd-MM-yyyy"));
			}
		}

		public void ShouldExist()
		{
			Assert.True(Exists, CallStack("Not found!"));
		}

		public void ShouldNotExist()
		{
			Assert.True(!Exists, CallStack("Not found!"));
		}

		public bool Exists
		{
			get
			{
				try
				{
					return Control != null;
				}
				catch
				{
					return false;
				}
			}
		}

		public bool Displayed
		{
			get
			{
				try
				{
					return Control.Displayed;
				}
				catch
				{
					return false;
				}
			}
		}

		public BaseElement FindElement(string xpath, int timeout = 0)
		{
			return FindElement(By.XPath(xpath), timeout);
		}

		public BaseElement FindElement(By by, int timeout = 0)
		{
			return FindElements(by, 1, timeout).FirstOrDefault();
		}

		public List<BaseElement> FindElements(string xpath, int count = 1, int timeout = 5)
		{
			return FindElements(By.XPath(xpath), count, timeout);
		}

		public List<BaseElement> FindElements(By by, int count = 1, int timeout = 5)
		{
			var result = new List<BaseElement>();
			result.AddRange(FindElements(Control, GetXPath(by), count, timeout));
			result.AddRange(FindElements(null, GetXPath(by), count, timeout));

			return result.Distinct().ToList();
		}

		private List<BaseElement> FindElements(IWebElement parent, string xpath, int count = 1, int timeout = 5)
		{
			try
			{
				if (parent != null && !xpath.StartsWith("."))
				{
					xpath = "." + xpath;
				}

				var by = By.XPath(xpath);

				if (parent == null)
				{
					new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout)).Until(driver => driver.FindElements(by).Count >= count);
					return _driver.FindElements(by).Select(element => new BaseElement(_driver, element, by)).ToList();
				}
				else
				{
					new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout)).Until(driver => parent.FindElements(by).Count >= count);
					return parent.FindElements(by).Select(element => new BaseElement(_driver, element, by)).ToList();
				}
			}
			catch
			{
				return new List<BaseElement>();
			}
		}

		public void Click(int? xOffset = null, int? yOffset = null)
		{
			xOffset = xOffset ?? (Control.TagName.Equals("a") ? 1 : Control.Size.Width / 2);
			yOffset = yOffset ?? (Control.TagName.Equals("a") ? 1 : Control.Size.Height / 2);

			new Actions(_driver).MoveToElement(Control, xOffset.Value, yOffset.Value).Click().Perform();
			BasePage.WaitForReady();
		}

		public string CallStack(string error)
		{
			return BasePage.CallStack($"{error} WebElement[{_by}]");
		}
	}
}
