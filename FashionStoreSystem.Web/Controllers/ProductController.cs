using FashionStoreSystem.Infrastructure.Extensions;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Services.Data.Models.Product;
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
        private readonly IProductService productService;
        private readonly IUserService userService;
        private readonly IFavoriteService favoriteService;
        public ProductController(
            ICategoryService categoryService,
            ISellerService sellerService, 
            IProductService productService,
            IUserService userService,
            IFavoriteService favoriteService)
        {
            this.categoryService = categoryService;
            this.sellerService = sellerService;
            this.productService = productService;
            this.userService = userService;
            this.favoriteService = favoriteService;

        }
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllProductsQueryModel queryModel)
        {
            AllProductsFilteredAndPagedServiceModel serviceModel = 
                await this.productService.AllAsync(queryModel); 

            queryModel.Products = serviceModel.Products;
            queryModel.TotalProducts = serviceModel.TotalProductsCount;
            queryModel.Categories = await this.categoryService.AllCategoryNamesAsync();

            return this.View(queryModel);
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
            try
            {
                ProductFormModel formModel = new ProductFormModel()
                {
                    Categories = await this.categoryService.AllCategoriesAsync(),
                };

                return View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductFormModel model)
        {
            bool isSeller = await this.sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isSeller)
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to add new products!";

                return this.RedirectToAction("Become", "Seller");
            }

            bool categoriesExists = await this.categoryService.ExistsByIdAsync(model.CategoryId);
            if (!categoriesExists) 
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Selected category does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }

            try
            {
                string? sellerId = 
                    await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);
                string productId = await this.productService.CreateAndReturnIdAsync(model, sellerId!);

                return this.RedirectToAction("Details", "Product", new { id = productId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new product! Please try again later or contanct administrator!");
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<ProductAllViewModel> myProducts = 
                new List<ProductAllViewModel>(); 

            string userId = this.User.GetId()!;
            bool isUserSeller = await this.sellerService
                .SellerExistsByUserIdAsync(userId);
            try
            {
                if (isUserSeller)
                {
                    string? sellerId = await this.sellerService
                        .GetSellerIdByUserIdAsync(userId);

                    myProducts.AddRange(await this.productService.AllBySellerIdAsync(sellerId!));
                }
                else
                {
                    myProducts.AddRange(await this.productService.AllByUserIdAsync(userId!));
                }

                return this.View(myProducts);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool productExists = await this.productService
                .ExistsByIdAsync(id);

            if (!productExists)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return this.RedirectToAction("All", "Product");
            }

            try
            {
                ProductDetailsViewModel viewModel = await this.productService
                    .GetDetailsByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool productExists = await this.productService
                .ExistsByIdAsync(id);

            if (!productExists)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return this.RedirectToAction("All", "Product");
            }

            bool isUserSeller = await this.sellerService
                .SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isUserSeller && !this.User.isAdmin())
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to edit product info!";

                return this.RedirectToAction("Become", "Seller");
            }

            string? sellerId = await this.sellerService
                .GetSellerIdByUserIdAsync(this.User.GetId()!);
            bool isSellerOwner = await this.productService
                .IsSellerWithIdOwnerOfProductWithIdAsync(id, sellerId!);

            if (!isSellerOwner && !this.User.isAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the seller owner of the product you want to edit!";

                return this.RedirectToAction("Mine", "Product");
            }

            try
            {
                ProductFormModel formModel = await this.productService
                    .GetProductForEditByIdAsync(id);
                formModel.Categories = await this.categoryService
                    .AllCategoriesAsync();

                return this.View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ProductFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }

            bool productExists = await this.productService
                .ExistsByIdAsync(id);

            if (!productExists)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return this.RedirectToAction("All", "Product");
            }

            bool isUserSeller = await this.sellerService
                .SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isUserSeller && !this.User.isAdmin())
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to edit product info!";

                return this.RedirectToAction("Become", "Seller");
            }

            string? sellerId = await this.sellerService
                .GetSellerIdByUserIdAsync(this.User.GetId()!);
            bool isSellerOwner = await this.productService
                .IsSellerWithIdOwnerOfProductWithIdAsync(id, sellerId!);

            if (!isSellerOwner && !this.User.isAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the seller owner of the product you want to edit!";

                return this.RedirectToAction("Mine", "Product");
            }

            try
            {
                await this.productService.EditProductByIdAndFormModelAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to update the product. Please try again later or contact administrator!");
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }

            return this.RedirectToAction("Details", "Product", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool productExists = await this.productService
                .ExistsByIdAsync(id);

            if (!productExists)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return this.RedirectToAction("All", "Product");
            }

            bool isUserSeller = await this.sellerService
                .SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isUserSeller && !this.User.isAdmin())
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to edit product info!";

                return this.RedirectToAction("Become", "Seller");
            }

            string? sellerId = await this.sellerService
                .GetSellerIdByUserIdAsync(this.User.GetId()!);
            bool isSellerOwner = await this.productService
                .IsSellerWithIdOwnerOfProductWithIdAsync(id, sellerId!);

            if (!isSellerOwner && !this.User.isAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the seller owner of the product you want to edit!";

                return this.RedirectToAction("Mine", "Product");
            }

            try
            {
                ProductPreDeleteDetailsViewModel viewModel = 
                    await this.productService.GetProductForDeleteByIdAsync(id);

                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, ProductPreDeleteDetailsViewModel model)
        {
            bool productExists = await this.productService
                .ExistsByIdAsync(id);

            if (!productExists)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return this.RedirectToAction("All", "Product");
            }

            bool isUserSeller = await this.sellerService
                .SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isUserSeller && !this.User.isAdmin())
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to edit product info!";

                return this.RedirectToAction("Become", "Seller");
            }

            string? sellerId = await this.sellerService
                .GetSellerIdByUserIdAsync(this.User.GetId()!);
            bool isSellerOwner = await this.productService
                .IsSellerWithIdOwnerOfProductWithIdAsync(id, sellerId!);

            if (!isSellerOwner && !this.User.isAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the seller owner of the product you want to edit!";

                return this.RedirectToAction("Mine", "Product");
            }

            try
            {
                await this.productService.GetProductForDeleteByIdAsync(id);

                this.TempData[WarningMessage] = "The product was successfully deleted!";
                return this.RedirectToAction("Mine", "Product");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Buy(string id)
        {
            bool isUserSeller = await this.sellerService
                .SellerExistsByUserIdAsync(this.User.GetId());

            if (isUserSeller)
            {
                this.TempData[ErrorMessage] = "Seller can't buy products! Please register as a user!";

                return this.RedirectToAction("Index", "Home");
            }

            bool productExists = await this.productService
                .ExistsByIdAsync(id);

            if (!productExists)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return this.RedirectToAction("All", "Product");
            }

            bool userHasEnoughMoney = await this.userService.UserHasEnoughMoneyToBuyProductAsync(id, this.User.GetId());

            if (!userHasEnoughMoney)
            {
                this.TempData[ErrorMessage] = "You don't have enough money to buy this product! If you have more money, you can add them in your wallet!";

                return this.RedirectToAction("Add", "User");
            }

            bool isProductInFavorite = await this.userService.IsProductInUserFavoriteAsync(id, this.User.GetId());
            if (isProductInFavorite)
            {
                await this.favoriteService.RemoveFromFavoriteAsync(id, this.User.GetId());
            }

            try
            {
                await this.userService.UserBuyProductAsync(id, this.User.GetId());

                TempData[SuccessMessage] = "Successfully purchased this product!";

                return this.RedirectToAction("All", "Product");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "Unexpected error occurred! Please try again or contact administrator!";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
