using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace TestContAcerta
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driverFf = new FirefoxDriver();

            driverFf.Url = "http://www.google.com";

            var searchBox = driverFf.FindElement(By.Id("gs_htif0"));
            searchBox.SendKeys("pluralsight");

            driverFf.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            var imgLink = driverFf.FindElements(By.ClassName("hdtb-mitem"))[0];
            imgLink.Click();
        }
    }
}
