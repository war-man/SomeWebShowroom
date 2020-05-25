using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomeWebShowroom.MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SomeWebShowroom.MVC.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles =WebConstants.AdminRole)]
        public async Task<IActionResult> Index()
        {
            var users = await this.userService.GetAll();

            return View(users);
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isAdmin = this.User.IsInRole(WebConstants.AdminRole);
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!isAdmin && currentUserId != id)
            {
                return Unauthorized();
            }

            var user = await this.userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }
              
            return View(user);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isAdmin = this.User.IsInRole(WebConstants.AdminRole);
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!isAdmin && currentUserId != id)
            {
                return Unauthorized();
            }

            var user = await this.userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await this.userService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
