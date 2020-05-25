namespace SomeWebShowroom.MVC.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SomeWebShowroom.MVC.Models;
    using SomeWebShowroom.MVC.Services;
    using SomeWebShowroom.MVC.Services.Models;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using static WebConstants;
    public class ProductsController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly IProductService productService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductsController(UserManager<User> userManager, IProductService productService, IWebHostEnvironment webHostEnvironment)
        {

            this.userManager = userManager;
            this.productService = productService;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await this.productService.GettAll());
        }

        [HttpGet]
        public async Task<IActionResult> FavoriteList()
        {
            var isAuhtenticated = this.User.Identity.IsAuthenticated;

            if (!isAuhtenticated)
            {
                return Unauthorized();
            }

            var user = await this.userManager.GetUserAsync(this.User);


            return View(await this.productService.GetFavoriteListByUser(user.Id));
        }

        // GET: Products/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.productService.GetProduct((int)id);

            if (product == null)
            {
                return NotFound();
            }

            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(this.User);

                var isInFavorite = await this.productService
                    .IsproductInFavoriteList(user.Id, product.Id);

                if (!isInFavorite)
                {
                    ViewBag.isFavorite = false;
                }
                else
                {
                    ViewBag.isFavorite = true;
                }
            }
            return View(product);
        }

        // GET: Products/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateProductRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description
                   // Image = UploadedFile(model.Image)
                };

                await this.productService.Create(product);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Products/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.productService.GetProduct((int)id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.productService.Update(product);

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.productService.GetProduct((int)id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.productService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddToFavoriteList(int id)
        {
            var isAuhtenticated = this.User.Identity.IsAuthenticated;

            if (!isAuhtenticated)
            {
                return Unauthorized();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.productService.AddTooFavoriteList(user.Id, id);
            TempData[TempDataSuccessMessageKey] = "Added to favorites.";
            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> RemoveFromFavoriteList(int id)
        {
            var isAuhtenticated = this.User.Identity.IsAuthenticated;

            if (!isAuhtenticated)
            {
                return Unauthorized();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.productService.RemoveFromFavoriteList(user.Id, id);
            TempData[TempDataSuccessMessageKey] = "Removed from favorites.";
            return RedirectToAction(nameof(Details), new { id = id });
        }

        private string UploadedFile(IFormFile file)
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
           var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, "as");
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

    }
}
