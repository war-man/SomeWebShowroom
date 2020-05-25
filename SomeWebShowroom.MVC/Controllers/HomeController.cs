namespace SomeWebShowroom.MVC.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SomeWebShowroom.MVC.Models;
    using SomeWebShowroom.MVC.Services;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IProductService productService;

        public HomeController(UserManager<User> userManager, IProductService productService)
        {
            this.userManager = userManager;
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var isAuhtenticated = this.User.Identity.IsAuthenticated;


            if (isAuhtenticated)
            {
                var user = await this.userManager.GetUserAsync(this.User);

                var userProducts = await this.productService.GetFavoriteListByUser(user.Id);
                return View(userProducts.Take(5));
            }
            var products = new List<Product>();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
