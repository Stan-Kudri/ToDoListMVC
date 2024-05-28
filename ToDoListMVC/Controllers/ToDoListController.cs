using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Core.Models.ToDoItem;
using ToDoList.Core.Repository;
using ToDoList.Models;

namespace ToDoListMVC.Controllers
{
    [Authorize(Roles = "User")]
    public class ToDoListController : Controller
    {
        private readonly ILogger<ToDoListController> _logger;
        private readonly ToDoItemsService _toDoItemsService;

        public ToDoListController(ILogger<ToDoListController> logger, ToDoItemsService caseService)
        {
            _logger = logger;
            _toDoItemsService = caseService;
        }

        [HttpGet]
        public IActionResult ViewToDo() => View();


        [HttpPost]
        public IActionResult ViewToDo(ToDoItemsModel item)
        {
            _toDoItemsService.Add(item);
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NoContent();
            }

            _toDoItemsService.Remove(id);
            return View("ViewToDo");
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
            => id != null && _toDoItemsService.TrySearchItem(id, out var item) && !string.IsNullOrEmpty(item.Description)
                ? View("Edit", item)
                : NoContent();

        [HttpPost]
        public IActionResult Edit(ToDoItems item)
        {
            if (item == null || string.IsNullOrEmpty(item.Description))
            {
                return NoContent();
            }

            _toDoItemsService.Update(item);
            return View("ViewToDo");
        }

        [HttpPost]
        public IActionResult ChangeExecution(Guid? id)
        {
            if (id != null)
            {
                _toDoItemsService.MarkCompleted(id);
                return View("ViewToDo");
            }

            return NoContent();
        }

        [HttpPost]
        public IActionResult ChangeDescription(Guid? id, string description)
        {
            if (id != null && !string.IsNullOrEmpty(description))
            {
                _toDoItemsService.UpdateDescription(id, description);
            }

            return View("ViewToDo");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
