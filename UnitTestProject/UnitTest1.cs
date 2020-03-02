using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddAdditionalCapability(CapabilityType.Version, "latest", true);
            options.AddAdditionalCapability(CapabilityType.Platform, "WIN10", true);
            options.AddAdditionalCapability("key", "key", true);
            options.AddAdditionalCapability("secret", "secret", true);
            options.AddAdditionalCapability("name", null, true);
            IWebDriver Driver = new ChromeDriver(Environment.CurrentDirectory);
            Driver.Navigate().GoToUrl("https://www.google.com");

            Driver.Manage().Window.FullScreen();
            // 3 | type | name=q | fff | 
            Driver.FindElement(By.Name("q")).SendKeys("fff");
            // 4 | click | id=lga |  | 
            Driver.FindElement(By.Id("lga")).Click();
            // 5 | click | css=center:nth-child(1) > .gNO89b |  | 
            Driver.FindElement(By.CssSelector("center:nth-child(1) > .gNO89b")).Click();
            // 6 | mouseOver | css=center:nth-child(1) > .gNO89b |  | 
            {
                IWebElement element = Driver.FindElement(By.CssSelector("center:nth-child(1) > .gNO89b"));
//                Action builder = new Actions(Driver);
  //              builder.MoveToElement(element).perform();
            }
            //Assert.;

        }
    }
}
