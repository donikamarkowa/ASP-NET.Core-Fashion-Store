using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreSystem.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("User/All")]
        public async Task<IActionResult> All()
        {
            IEnumerable<UserViewModel> viewModel = await this.userService
                .AllAsync();

            return View(viewModel); 
        }
    }
}
