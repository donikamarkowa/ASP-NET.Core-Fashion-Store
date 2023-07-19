using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreSystem.Web.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        public async Task<IActionResult> Become()
        {
            return View();
        }
    }
}
