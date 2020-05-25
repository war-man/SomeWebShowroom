namespace SomeWebShowroom.Test.Controllers
{
    using BGRent.Server.Features.Identity;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;
    using static SomeWebShowroom.MVC.WebConstants;
    using static Helpers.GetMock;
    using Microsoft.AspNetCore.Mvc;
    using FluentAssertions;
    using SomeWebShowroom.MVC.Services.Models;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class IdentityControllerTest
    {
        [Fact]
        public void RegisterGet_ShouldReturn_ViewPage()
        {
            //arrange
            var identityController = new IdentityController(null, null, null);

            //act
            var result = identityController.Register();

            //assert
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task RegisterPostWithInvalidModel_ShouldReturn_ViewWithModel()
        {
            //arrange
            var userManagerMock = UserManagerMock();
            var identityServiceMock = IdentityServiceMock();
            var signinManagerMock = SignInManagerMock();

            var identityController = new IdentityController(
                userManagerMock.Object, signinManagerMock.Object, identityServiceMock.Object);
            identityController.ModelState.AddModelError("", "");

            var model = new RegisterRequestModel();


            //act

            var results = await identityController.Register(model);

            //assert

            results.Should().BeOfType<ViewResult>()
                .Subject
                .Model.Should().BeOfType<RegisterRequestModel>();
        }

        [Theory]
        [ClassData(typeof(TestData.ModelTestData))]
        public void PropertyWithIncorrectData_ShouldReturn_ErrorMessage(string data, string property, string expectedError)
        {
            //arrange
            var result = new List<ValidationResult>();

            //act
            var isValid = Validator.TryValidateProperty(
               data, new ValidationContext(new RegisterRequestModel()) { MemberName = property }, result);

            //assert

            isValid.Should().BeFalse();

            result
                .First().ErrorMessage.ToString()
                .Should().Be(expectedError);    
        }
    }
}
