using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;

namespace KayakTest.Steps
{
    [Binding]
    public sealed class MainPageSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public MainPageSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the main page is open")]
        public void GivenTheMainPageIsOpen()
        {
            var webDriver = CreateDriver();
            _scenarioContext.Add("driver", webDriver);
        }

        private static RemoteWebDriver CreateDriver()
        {
            var webDriver = new 
                //EdgeDriver
                ChromeDriver(@"C:\Users\culud\Downloads", new ChromeOptions(), TimeSpan.FromSeconds(120));//my laptop is too slow, giving timeouts for default command timeout
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);//probably my bad internet connection
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl("http://kayak.com");

            //if there is redirection from an international page, click it:
            var redirectLinks = webDriver.FindElementsByClassName("UKS8-redirect-link");
            if (redirectLinks.Count > 0)
                redirectLinks[0].Click();

            return webDriver;
        }

        [When(@"I enter (.*) in From field")]
        public void WhenIEnter_InFromField(string from)
        {
            var fromField = GetDriver().FindElementByClassName("lNCO-inner");
            ClickChangingElement(fromField);

            var textBox = By.ClassName("k_my-input");
            new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementToBeClickable(textBox))
                .SendKeys(from);
        }

        [When(@"I click Stays")]
        public void WhenIClickStays()
        {
            GetDriver().FindElementByLinkText("Stays").Click();
        }

        private static void ClickChangingElement(IWebElement element)
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    element.Click();
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine(element + " stale, try to click again..");
                    Thread.Sleep(1000);
                }
            }
            Console.WriteLine(element + " could not be clicked, don't give up!");
        }

        [When(@"I select first tip")]
        public void WhenISelectFirstTip()
        {
            var firstTip = By.XPath("//DIV[@class='c8GSD-overlay-dropdown']//LI[2]");
            new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementToBeClickable(firstTip))
                .Click();

        }

        [When(@"I hit search button")]
        public void WhenIHitSearchButton()
        {
            GetDriver()
                //.FindElementByClassName("NbWx-button")
                .FindElementByXPath("//*[@type='submit']")
                .Click();
        }

        [Then(@"I see error (.*)")]
        public void ThenISeeError(string errorMessage)
        {
            GetDriver().FindElementByClassName("IGR4-error").Text.Should().Be(errorMessage);
        }

        [When(@"I select different drop-off")]
        public void WhenISelectDifferentDrop_Off()
        {
            var dropOffSelection = By.ClassName("wIIH-mod-alignment-left");
            ClickChangingElement(GetDriver().FindElement(dropOffSelection));
            var differentDropOff = By.XPath("//LI[@role='tab'][2]");
            new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(differentDropOff));
            GetDriver().FindElement(differentDropOff).Click();
        }

        private RemoteWebDriver GetDriver()
        {
            return _scenarioContext.Get<RemoteWebDriver>("driver");
        }

        [AfterScenario()]
       public void AfterScenario()
        {
            GetDriver().Quit();
        }
    }

}
