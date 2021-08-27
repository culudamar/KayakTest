using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace KayakTest.Util
{
    /// <summary>
    /// Base class that is extended by all the Step classes. Includes common fields and methods.
    /// </summary>
    public class StepBase
    {
        protected readonly ScenarioContext _scenarioContext;
        public StepBase(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        protected RemoteWebDriver GetDriver()
        {
            return _scenarioContext.Get<RemoteWebDriver>("driver");
        }

        protected static void ClickChangingElement(IWebElement element)
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

        [AfterScenario()]
        public void AfterScenario()
        {
            GetDriver().Quit();
        }
    }
}
