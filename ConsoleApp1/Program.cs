using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            //start chrome using command: "C:\Program Files\Google\Chrome\Application\chrome.exe" http://kayak.com --new-window --remote-debugging-port=5555 --user-data-dir="C:\temp"

            var options = new ChromeOptions
            {
                DebuggerAddress = "127.0.0.1:5555"
            };

            var d = new ChromeDriver("c:\\temp", options);

            try
            {
                var text = d.FindElement(By.ClassName("EuxN-taxes")).Text;
                Console.WriteLine(text);
            }
            finally
            {
                d.Quit();

            }
        }
    }
}
