using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using KayakTest.Util;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace KayakTest.Steps
{
    [Binding]
    public sealed class ResultPageSteps : StepBase
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        public ResultPageSteps(ScenarioContext scenarioContext):base(scenarioContext)
        {
        }

        [Then(@"the result page should open")]
        public void ThenTheResultPageShouldOpen()
        {
            var text = GetDriver().FindElementByXPath("//DIV[@class='EuxN-Taxes']").Text;
            text.Should().Be("Total");
        }
    }
}
