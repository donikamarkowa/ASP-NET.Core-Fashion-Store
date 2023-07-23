using FashionStoreSystem.Infrastructure.Extensions;
using FashionStoreSystem.Services.Data;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.ViewModels.Favorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FashionStoreSystem.Common.NotificationMessagesConstants;

namespace FashionStoreSystem.Web.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService favoriteService;
        private readonly IProductService productService;
        private readonly ISellerService sellerService;

        public FavoriteController(IFavoriteService favoriteService, IProductService productService, ISellerService sellerService)
        {
            this.favoriteService = favoriteService;
            this.productService = productService;
            this.sellerService = sellerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorite(string id)
        {
            bool productExist = await this.productService
                .ExistsByIdAsync(id);

            if (!productExist)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return this.RedirectToAction("All", "Product");
            }

            bool userHasFavorite = await this.favoriteService
                .IsProductInFavoriteByUserIdAsync(id, this.User.GetId());

            if (userHasFavorite)
            {
                this.TempData[ErrorMessage] = "Selected product is already added to favorite by you!";

                return this.RedirectToAction("All", "Product");
            }

            bool isUserSeller = await this.sellerService
                .SellerExistsByUserIdAsync(this.User.GetId());

            if (isUserSeller)
            {
                this.TempData[ErrorMessage] = "Seller can't add products to favorite! Please register as a user!";

                return this.RedirectToAction("Index", "Home");
            }

            try
            {
                await this.favoriteService.AddToFavoriteAsync(id, this.User.GetId());

                this.TempData[SuccessMessage] = "Successfully added the product to Favorite!";

                return this.RedirectToAction("All", "Product");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorite(string id)
        {
            bool productExists = await this.productService
                .ExistsByIdAsync(id);

            if (!productExists)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return this.RedirectToAction("All", "Product");
            }

            bool userHasFavorite = await this.favoriteService
                .IsProductInFavoriteByUserIdAsync(id, this.User.GetId());

            if (!userHasFavorite)
            {
                this.TempData[ErrorMessage] = "This product is not in the user favorite products!";

                return this.RedirectToAction("All", "Product");
            }

            bool isUserSeller = await this.sellerService
                .SellerExistsByUserIdAsync(this.User.GetId());

            if (isUserSeller)
            {
                this.TempData[ErrorMessage] = "Sellers can't remove favorite products! Please register as a user!";

                return this.RedirectToAction("Index", "Home");
            }

            try
            {
                await this.favoriteService.RemoveFromFavoriteAsync(id, this.User.GetId());

                this.TempData[SuccessMessage] = "Successfully remove the product from Favorite!";

                return this.RedirectToAction("MyFavorite", "Favorite");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Favorite()
        {
            bool isUserSeller = await this.sellerService
                .SellerExistsByUserIdAsync(this.User.GetId());

            if (isUserSeller)
            {
                this.TempData[ErrorMessage] = "Seller don't have favorite products! Please register as a user!";

                return this.RedirectToAction("Index", "Home");
            }

            List<FavoriteViewModel> myFavoriteProducts = new List<FavoriteViewModel>();

            try
            {
                myFavoriteProducts.AddRange(await this.favoriteService.GetFavoriteByUserIdAsync(this.User.GetId()));

                return this.View(myFavoriteProducts);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator!";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
