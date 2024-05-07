using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Core.Models;
using ToDoList.Core.Models.Affair;
using ToDoList.Core.Repository;
using ToDoList.Models;

namespace ToDoListMVC.Controllers
{
    [Authorize(Roles = "User")]
    public class ToDoListController : Controller
    {
        private readonly ILogger<ToDoListController> _logger;
        private readonly AffairsService _affairsService;

        public ToDoListController(ILogger<ToDoListController> logger, AffairsService caseService)
        {
            _logger = logger;
            _affairsService = caseService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ViewToDo([FromServices] ICurrentUserAccessor currentUserAccessor)
            => currentUserAccessor.UserId is null ? RedirectToAction("SignIn", "Authentication") : View();


        [HttpPost]
        public IActionResult ViewToDo(AffairsModel item)
        {
            _affairsService.Add(item);
            return RedirectToAction();
        }

        [HttpPost]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NoContent();
            }

            _affairsService.Remove(id);
            return RedirectToAction("ViewToDo");
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
            => id != null && _affairsService.TrySearchItem(id, out var item)
                ? View("Edit", item)
                : NoContent();

        [HttpPost]
        public IActionResult Edit(Affairs item)
        {
            if (item == null)
            {
                return NoContent();
            }

            _affairsService.Update(item);
            return RedirectToAction("ViewToDo");
        }

        [HttpPost]
        public IActionResult ChangeExecution(Guid? id)
        {
            if (id != null)
            {
                _affairsService.MarkCompleted(id);
                return RedirectToAction("ViewToDo");
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult ChangeDescription(Guid? id, string description)
        {
            if (id != null)
            {
                _affairsService.UpdateDescription(id, description);
                return RedirectToAction("ViewToDo");
            }

            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
