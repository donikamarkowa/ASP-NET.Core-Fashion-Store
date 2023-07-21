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
        public ProductController(ICategoryService categoryService, ISellerService sellerService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.sellerService = sellerService;
            this.productService = productService;
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

            ProductFormModel formModel = new ProductFormModel()
            {
                Categories = await this.categoryService.AllCategoriesAsync(),
            };

            return View(formModel);
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
                await this.productService.CreateAsync(model, sellerId!);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new product! Please try again later or contanct administrator!");
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }

            return this.RedirectToAction("All", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<ProductAllViewModel> myProducts = 
                new List<ProductAllViewModel>(); 

            string userId = this.User.GetId()!;
            bool isUserSeller = await this.sellerService
                .SellerExistsByUserIdAsync(userId);

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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            ProductDetailsViewModel? viewModel = await this.productService
                .GetDetailsByIdAsync(id);

            if (viewModel == null)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return this.RedirectToAction("All", "Product");
            }

            return View(viewModel);
        }
    }
}
