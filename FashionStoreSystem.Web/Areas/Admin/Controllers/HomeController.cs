﻿using Microsoft.AspNetCore.Mvc;

namespace FashionStoreSystem.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
