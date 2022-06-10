using System;
using FluentAssertions;
using KayakTest.Util;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;

namespace KayakTest.Steps
{
    [Binding]
    public sealed class MainPageSteps : StepBase
    {

        public MainPageSteps(ScenarioContext scenarioContext):base(scenarioContext)
        {
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
                ChromeDriver(@"C:\temp", new ChromeOptions(), TimeSpan.FromSeconds(120));//my laptop is too slow, giving timeouts for default command timeout
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
            var fromField = GetDriver().FindElementByClassName("k_my-input");
            ClickChangingElement(fromField);

            var textBox = By.ClassName("k_my-input");
            new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementToBeClickable(textBox))
                .SendKeys(from);
        }

        [When(@"I click (.*)")]
        public void WhenIClickStays(string linkText)
        {
            GetDriver().FindElementByLinkText(linkText).Click();
        }

        [When(@"I select first tip")]
        public void WhenISelectFirstTip()
        {
            var firstTip = By.ClassName("JyN0-picture");
            new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementToBeClickable(firstTip))
                .Click();

        }

        [When(@"I hit search button")]
        public void WhenIHitSearchButton()
        {
            GetDriver()
                //.FindElementByClassName("NbWx-button") // didn't work for different drop-off
                .FindElementByXPath("//*[@type='submit']")
                .Click();
        }

        [Then(@"I see error (.*)")]
        public void ThenISeeError(string errorMessage)
        {
            var error = By.ClassName("IGR4-error");
            new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(error));
            GetDriver().FindElement(error).Text.Should().Be(errorMessage);
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

    }

}
