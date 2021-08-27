using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace KayakTest.Steps
{
    [Binding]
    public sealed class ResultPageSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public ResultPageSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"the result page should open")]
        public void ThenTheResultPageShouldOpen()
        {
            var text = GetDriver().FindElementByXPath("//DIV[@class='EuxN-Taxes']").Text;
            text.Should().Be("Total");
        }
        private RemoteWebDriver GetDriver()
        {
            return _scenarioContext.Get<RemoteWebDriver>("driver");
        }
    }
}
