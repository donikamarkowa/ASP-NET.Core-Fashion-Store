using FashionStoreSystem.Infrastructure.Extensions;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.Areas.Admin.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreSystem.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseAdminController
    {
        private readonly IProductService productService;
        private readonly ISellerService sellerService;

        public ProductController(IProductService productService, ISellerService sellerService)
        {
            this.productService = productService;
            this.sellerService = sellerService;
        }
        public async Task<IActionResult> Mine()
        {
            string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);
            MyProductsViewModel viewModel = new MyProductsViewModel()
            {
                AddedProducts = await this.productService.AllBySellerIdAsync(sellerId!)
            };

            return this.View(viewModel);
        }
    }
}
