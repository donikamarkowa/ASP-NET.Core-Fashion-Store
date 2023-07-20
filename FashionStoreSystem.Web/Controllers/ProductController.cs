using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreSystem.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

        }
    }
}
