using FashionStoreSystem.Infrastructure.Extensions;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FashionStoreSystem.Common.NotificationMessagesConstants;

namespace FashionStoreSystem.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ISellerService sellerService;
        public ProductController(ICategoryService categoryService, ISellerService sellerService)
        {
            this.categoryService = categoryService;
            this.sellerService = sellerService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isSeller = await this.sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isSeller)
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to add new products!";

                return this.RedirectToAction("Become", "Seller");
            }

            ProductFormModel formModel = new ProductFormModel()
            {
                Categories = await this.categoryService.AllCategoriesAsync(),
            };

            return View(formModel);
        }
    }
}
