using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.ViewModels.Purchase;
using FashionStoreSystem.Web.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreSystem.Web.Areas.Admin.Controllers
{
    public class PurchaseController : BaseAdminController
    {
        private readonly IPurchaseService purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }
        public async Task<IActionResult> All()
        {
            IEnumerable<PurchaseViewModel> viewModel = await this.purchaseService
                .AllAsync();

            return View(viewModel); 
        }
    }
}
