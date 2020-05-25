namespace SomeWebShowroom.Test.Helpers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using SomeWebShowroom.MVC.Models;
    using SomeWebShowroom.MVC.Services;

    public static class GetMock
    {
        public static Mock<UserManager<User>> UserManagerMock()
        {
            return new Mock<UserManager<User>>(
                 Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        }

        public static Mock<IProductService> ProductServiceMock()
        {
            return new Mock<IProductService>();
        }

        public static Mock<SignInManager<User>> SignInManagerMock()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();

            return new Mock<SignInManager<User>>(
                UserManagerMock().Object,
                httpContextAccessor.Object,
                userPrincipalFactory.Object, null, null, null, null);
        }

        public static Mock<IIdentityService> IdentityServiceMock()
        {
            return new Mock<IIdentityService>();
        }
    }
}
