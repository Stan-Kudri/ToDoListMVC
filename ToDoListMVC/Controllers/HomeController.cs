using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
        public const string NameHomePage = "HomePage";
        public const string NameHomeController = "Home";

        public IActionResult HomePage() => View();
    }
}
