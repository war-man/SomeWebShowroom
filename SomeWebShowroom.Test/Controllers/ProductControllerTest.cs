namespace SomeWebShowroom.Test.Controllers
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using SomeWebShowroom.MVC;
    using SomeWebShowroom.MVC.Controllers;
    using SomeWebShowroom.MVC.Models;
    using SomeWebShowroom.MVC.Services.Models;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;
    using static SomeWebShowroom.Test.Helpers.GetMock;

    public class ProductControllerTest
    {
        [Fact]
        public void CreateProductGet_ShouldBeAccessableOnly_ByAdministrators()
        {
            //Arrange
            var method = GetPostMethodInfo("Create");

            // Act
            var atributes = method.GetCustomAttributes(true);
            var result = atributes
                 .Where(x => x.GetType() == typeof(AuthorizeAttribute))
                .SelectMany(attrib => ((AuthorizeAttribute)attrib)
                .Roles.Split(new[] { ',' }))
                .ToList();

            //Assert
            result
                .Should().Contain(WebConstants.AdminRole)
                .And
                .NotContain(WebConstants.UserRole);
        }

        [Fact]
        public void CreateProductPost_ShouldBeAccessableOnly_ByAdministrators()
        {
            //Arrange
            var method = GetGetMethodInfo("Create");

            // Act
            var atributes = method.GetCustomAttributes(true);
            var result = atributes
                 .Where(x => x.GetType() == typeof(AuthorizeAttribute))
                .SelectMany(attrib => ((AuthorizeAttribute)attrib)
                .Roles.Split(new[] { ',' }))
                .ToList();

            //Assert
            result
                .Should().Contain(WebConstants.AdminRole)
                 .And
                .NotContain(WebConstants.UserRole);
        }

        [Fact]
        public void CreateProductGet_ShouldReturn_ViewPage()
        {
            //arrange
            var productsController = new ProductsController(null, null, null);

            //act
            var result = productsController.Create();

            //assert
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task CreateProductPost_ShouldReturn_RedirectToActionIndex()
        {
            //arrange
            var userManagerMock = UserManagerMock();
            var productServiceMock = ProductServiceMock();

            var productsController = new ProductsController(userManagerMock.Object, productServiceMock.Object, null);
            var model = new CreateProductRequestModel();
            //act
            var result = await productsController.Create(model);

            //assert
            result
                .Should().BeOfType<RedirectToActionResult>()
                .Subject
                .ActionName
                .Should()
                .Be(nameof(Index));
        }

        [Fact]
        public async Task CreateProductPostWithInvalidModelState_ShouldReturn_ViewPageWithCtreateProductRequestModel()
        {
            //arrange
            var userManagerMock = UserManagerMock();
            var productServiceMock = ProductServiceMock();

            var productsController = new ProductsController(userManagerMock.Object, productServiceMock.Object, null);
            var product = new CreateProductRequestModel();
            productsController.ModelState.AddModelError("", "");
            //act
            var result = await productsController.Create(product);

            //assert
            result
                .Should().BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .BeOfType<CreateProductRequestModel>();
        }

        private MethodInfo GetGetMethodInfo(string methodName)
        {
            return typeof(ProductsController)
                .GetMethods()
                .Where(x => x.CustomAttributes
                .Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .ToList()
            .FirstOrDefault(x => x.Name == methodName);
        }

        private MethodInfo GetPostMethodInfo(string methodName)
        {
            return typeof(ProductsController)
                .GetMethods()
                .Where(x => x.CustomAttributes
                .Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .ToList()
            .FirstOrDefault(x => x.Name == methodName);
        }
    }
}
