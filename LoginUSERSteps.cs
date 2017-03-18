using System;
using TechTalk.SpecFlow;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

namespace Test
{
    [Binding]
    public class LoginUSERSteps
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;

        [Given(@"Setup")]
        public void GivenSetup()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
            service.FirefoxBinaryPath = @"D:\AboveGoal\AutomationTestsLM\Test\geckodriver.exe";
            FirefoxDriver driver = new FirefoxDriver(service);
            //driver = new ChromeDriver();
            baseURL = "http://abovegoal.azurewebsites.net";
            verificationErrors = new StringBuilder();
        }
        
        [Given(@"Fillinformationfromtheuser")]
        public void GivenFillInformationFromTheUser()
        {
            driver.Navigate().GoToUrl(baseURL + "/Account/Login/?ReturnUrl=%2Fapp%2F");
            driver.FindElement(By.Name("tenancyName")).Clear();
            driver.FindElement(By.Name("tenancyName")).SendKeys("default");
            driver.FindElement(By.Name("usernameOrEmailAddress")).Clear();
            driver.FindElement(By.Name("usernameOrEmailAddress")).SendKeys("admin");
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("123qwe");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Id("HeaderCurrentUserName"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            driver.FindElement(By.Id("HeaderCurrentUserName")).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Id("UserProfileMySettingsLink"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
        }
        
        [When(@"Checksteps")]
        public void WhenCheckSteps()
        {
            driver.FindElement(By.Id("UserProfileMySettingsLink")).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Id("UserProfileMySettingsLink"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            Assert.AreEqual("My settings", driver.FindElement(By.Id("UserProfileMySettingsLink")).Text);
            driver.FindElement(By.Id("UserProfileMySettingsLink")).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Name("Name"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            Assert.AreEqual("admin", driver.FindElement(By.Name("Name")).GetAttribute("value"));
            Assert.AreEqual("admin", driver.FindElement(By.Name("Surname")).GetAttribute("value"));
            Assert.AreEqual("admin@defaulttenant.com", driver.FindElement(By.Name("EmailAddress")).GetAttribute("value"));
            driver.FindElement(By.XPath("(//button[@type='button'])[5]")).Click();
        }
        
        [Then(@"Ifallstepsarecorrectlycompletedthendriverwillclosethebrowser")]
        public void ThenIfAllStepsAreCorrectlyCompletedThenDriverWillCloseTheBrowser()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        private bool IsElementPresent(By by)
        {
            throw new NotImplementedException();
        }
    }
}

