﻿using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        public const string NameHomePage = "HomePage";
        public const string NameHomeController = "Home";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
            => _logger = logger;

        public IActionResult HomePage() => View();
    }
}
