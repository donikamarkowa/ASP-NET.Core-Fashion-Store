using FashionStoreSystem.Infrastructure.Extensions;
using FashionStoreSystem.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                return BadRequest();
            }

            return View();
        }
    }
}
