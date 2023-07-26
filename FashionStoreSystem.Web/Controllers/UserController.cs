using FashionStoreSystem.Infrastructure.Extensions;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FashionStoreSystem.Common.NotificationMessagesConstants;

namespace FashionStoreSystem.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> MyWallet()
        {
            string userId = this.User.GetId();
            bool userExists = await this.userService.UserExistsByIdAsync(userId);

            if (!userExists)
            {
                this.TempData[ErrorMessage] = "User with the provided id does not exist!";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMoney(UserViewModel model)
        {
            string userId = this.User.GetId();
            bool userExists = await this.userService.UserExistsByIdAsync(userId);

            if (!userExists)
            {
                this.TempData[ErrorMessage] = "User with the provided id does not exist!";

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
