using FluentAssertions;
using KayakTest.Util;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;

namespace KayakTest.Steps
{
    [Binding]
    public class StaySearchStepDefinitions:StepBase
    {
        public StaySearchStepDefinitions(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [Then(@"the stay result page should open")]
        public void ThenTheStayResultPageShouldOpen()
        {
            var elementToCheck = By.ClassName("kzGk-resultInner");

            new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(30))
                .Until(ExpectedConditions.ElementIsVisible(elementToCheck));

            var text = GetDriver().FindElement(elementToCheck).Text;
            text.Should().Contain("Location");
        }
    }
}
