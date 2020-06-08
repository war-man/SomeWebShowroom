namespace SomeWebShowroom.Test.UITests
{
    using FluentAssertions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;
    using Xunit;

    public class AutomatedUITest : IDisposable
    {
        private readonly IWebDriver driver;
        public AutomatedUITest()
        {
            this.driver = new ChromeDriver();
        }

        public void Dispose()
        {
            this.driver.Quit();
            this.driver.Dispose();
        }

        [Fact]
        public void NavigateToHome_Shouldopen_HomePage()
        {
           this. driver.Navigate()
                .GoToUrl("https://localhost:44393");

            var result = this.driver.Title;

            result
                .Should()
                .Be("Home Page");
        }
    }
}
