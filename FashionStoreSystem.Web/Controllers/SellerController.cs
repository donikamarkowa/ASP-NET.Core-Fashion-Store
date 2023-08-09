using FashionStoreSystem.Infrastructure.Extensions;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.ViewModels.Seller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FashionStoreSystem.Common.NotificationMessagesConstants;

namespace FashionStoreSystem.Web.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        private readonly ISellerService sellerService;
        public SellerController(ISellerService sellerService)
        {
            this.sellerService = sellerService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string userId = this.User.GetId();
            bool isSeller = await this.sellerService.SellerExistsByUserIdAsync(userId);   

            if (isSeller) 
            {
                this.TempData[ErrorMessage] = "You are already a seller!";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeSellerFormModel model)
        {
            string? userId = this.User.GetId();
            bool isSeller = await this.sellerService.SellerExistsByUserIdAsync(userId);

            if (isSeller)
            {
                this.TempData[ErrorMessage] = "You are already a seller!";

                return RedirectToAction("Index", "Home");
            }

            bool isPhoneNumberTaken = await this.sellerService.SellerExistsByPhoneNumberAsync(model.PhoneNumber);

            if (isPhoneNumberTaken)
            {
                this.ModelState.AddModelError(nameof(model.PhoneNumber), "Seller with the provided phone number already exists!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            bool userHasProducts = await this.sellerService.HasProductsByUserIdAsync(userId);

            if (userHasProducts)
            {
                TempData[ErrorMessage] = "You don't need to have products to become a seller!";

                return this.RedirectToAction("Mine", "Product");
            }

            try
            {
                await this.sellerService.Create(userId, model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred while registering you as a seller! Please try again later or contact administrator!";

                return this.RedirectToAction("Index", "Home");  
            }

            return this.RedirectToAction("All", "Product");
        }
    }
}
