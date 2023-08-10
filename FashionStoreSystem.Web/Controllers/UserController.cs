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
        private readonly ISellerService sellerService;

        public UserController(IUserService userService, ISellerService sellerService)
        {
            this.userService = userService;
            this.sellerService = sellerService;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            string userId = this.User.GetId();
            bool userExists = await this.userService.UserExistsByIdAsync(userId);

            if (!userExists)
            {
                this.TempData[ErrorMessage] = "User with the provided id does not exist!";

                return RedirectToAction("Index", "Home");
            }

            bool isUserSeller = await this.sellerService.SellerExistsByUserIdAsync(userId); 

            if (isUserSeller)
            {
                this.TempData[ErrorMessage] = "Sellers can't add money to their wallet. They only get their earned money there.";
            }

            decimal currentWalletBalance = await this.userService.GetWalletBalanceByUserIdAsync(userId);

            var userViewModel = new UserWalletViewModel
            {
                Id = userId,
                Wallet = currentWalletBalance
            };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserWalletViewModel model)
        {
            string userId = this.User.GetId();
            bool userExists = await this.userService.UserExistsByIdAsync(userId);

            if (!userExists)
            {
                this.TempData[ErrorMessage] = "User with the provided id does not exist!";

                return RedirectToAction("Index", "Home");
            }

            bool isUserSeller = await this.sellerService.SellerExistsByUserIdAsync(userId);

            if (isUserSeller)
            {
                this.TempData[ErrorMessage] = "Sellers can't add money to their wallet. They only get their earned money there.";
            }

            try
            {
                await this.userService.AddMoneyToWallet(this.User.GetId(), model.Wallet);

                this.TempData[SuccessMessage] = "You successfully added money to your wallet!";

                return RedirectToAction("All", "Product");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add money to your wallet! Please try again later or contact administrator!");

                return this.View(model);
            }
        }
    }
}
