namespace SomeWebShowroom.Test.UITests
{
    using FluentAssertions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.PageObjects;
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

        [Theory]
        [ClassData(typeof(TestData.NavigateToPageTestData))]
        public void NavigateToPage_Shouldopen_TheRightPage(string url, string title)
        {
            //Arrange
           this. driver.Navigate()
                .GoToUrl("https://localhost:44393/" + url);

            //Act
            var result = this.driver.Title;

            //Assert
            result
                .Should()
                .Be(title);
        }


        //[Fact]
        //public void Create_WhenSuccessfullyExecuted_ReturnsIndexViewWithNewEmployee()
        //{
        //    this.driver.Navigate()
        //        .GoToUrl("https://localhost:44393/Identity/register");

        //    this.driver.FindElement(By.Id("Email"))
        //        .SendKeys("");
        //    this.driver.FindElement(By.Id("Username"))
        //        .SendKeys("admin");

        //    this.driver.FindElement(By.Id("submit"))
        //        .Click();

        //    // Assert.Equal("Register", this.driver.Title);
        //    // Assert.Contains("Email is required", this.driver.PageSource);
        //    var result = driver.FindElement(By.Id("Email")).GetAttribute("Required").
        //    Assert.Equal("Email is required", result);
        //}
    }
}
