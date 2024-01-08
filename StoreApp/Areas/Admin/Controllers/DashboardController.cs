﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Areas.Admin.Controllers
{

    [Area("Admin")] //Area controller'ın başına yazmak gerekiyor
    [Authorize(Roles = "Admin")]
    public class DashboardController: Controller
    {
        public IActionResult Index() {

            TempData["info"] = $"Welcome Back, { DateTime.Now.ToShortTimeString() }";
            return View(); 
        }

    }
}
