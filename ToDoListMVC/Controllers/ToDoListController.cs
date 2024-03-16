using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Core.Repository;
using ToDoList.Models;

namespace ToDoListMVC.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly ILogger<ToDoListController> _logger;
        private readonly AffairsService _caseService;

        public ToDoListController(ILogger<ToDoListController> logger, AffairsService caseService)
        {
            _logger = logger;
            _caseService = caseService;
        }

        [HttpGet]
        public IActionResult Table()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewToDo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ViewToDo(AffairsModel item)
        {
            var caseItem = new Affairs(item.description, DateTime.Now, item.isCaseCompletion, item.isCaseCompletion == true ? DateTime.Now : null);
            _caseService.Add(caseItem);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
