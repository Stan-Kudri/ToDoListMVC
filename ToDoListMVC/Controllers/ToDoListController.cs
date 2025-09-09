using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Core.Models.ToDoItem;
using ToDoList.Core.Repository;
using ToDoList.Models;

namespace ToDoListMVC.Controllers
{
    [Authorize(Roles = "User")]
    public class ToDoListController(ILogger<ToDoListController> logger, ToDoItemsService caseService) : Controller
    {
        public const string NameEditPage = "Edit";
        public const string NameViewToDoPage = "ViewToDo";
        public const string NameToDoListController = "ToDoList";

        [HttpGet]
        public IActionResult ViewToDo() => View();

        [HttpPost]
        public IActionResult ViewToDo(ToDoItemsModel item)
        {
            caseService.Add(item);
            ModelState.Clear();
            return View(NameViewToDoPage);
        }

        [HttpPost]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NoContent();
            }

            caseService.Remove(id);
            return View(NameViewToDoPage);
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
            => id != null && caseService.TrySearchItem(id, out var item) && !string.IsNullOrEmpty(item?.Description)
                ? View(NameEditPage, item)
                : NoContent();

        [HttpPost]
        public IActionResult Edit(ToDoItems item)
        {
            if (item == null || string.IsNullOrEmpty(item.Description))
            {
                return NoContent();
            }

            caseService.Update(item);
            ModelState.Clear();
            return View(NameViewToDoPage);
        }

        [HttpPost]
        public IActionResult ChangeExecution(Guid? id)
        {
            if (id != null)
            {
                caseService.MarkCompleted(id);
                return View(NameViewToDoPage);
            }

            return NoContent();
        }

        [HttpPost]
        public IActionResult ChangeDescription(Guid? id, string description)
        {
            if (id != null && !string.IsNullOrEmpty(description))
            {
                caseService.UpdateDescription(id, description);
            }

            ModelState.Clear();
            return View(NameViewToDoPage);
        }

        [HttpGet]
        public IActionResult Close() => RedirectToAction(NameViewToDoPage, NameToDoListController);

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
