using Microsoft.AspNetCore.Mvc;

namespace ToDoListMVC.Controllers
{
    public class PersonalDateController : Controller
    {
        public IActionResult PersonalDate()
        {
            return View();
        }
    }
}
