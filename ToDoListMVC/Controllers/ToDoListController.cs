using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Core.Models;
using ToDoList.Core.Models.Users;
using ToDoList.Core.Repository;
using ToDoList.Models;

namespace ToDoListMVC.Controllers
{
    [Authorize(Roles = "User")]
    public class ToDoListController : Controller
    {
        private readonly ILogger<ToDoListController> _logger;
        private readonly AffairsService _affairsService;
        private readonly User _user;

        public ToDoListController(ILogger<ToDoListController> logger, AffairsService caseService, User user)
        {
            _logger = logger;
            _affairsService = caseService;
            _user = user;
        }

        [HttpGet]
        public IActionResult ViewToDo()
            => View();

        [HttpPost]
        public IActionResult ViewToDo(AffairsModel item)
        {
            var affairsModel = new Affairs(
                                            item.description,
                                            DateTime.Now,
                                            item.isCaseCompletion,
                                            item.isCaseCompletion == true ? DateTime.Now : null,
                                            item.userId);

            _affairsService.Add(affairsModel);
            return RedirectToAction();
        }

        [HttpPost]
        public IActionResult Delete(Guid? id)
        {
            if (id != null)
            {
                if (_affairsService.TrySearchItem(id, out var item))
                {
                    _affairsService.Remove(item);
                }

                return RedirectToAction("ViewToDo");
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            if (id != null)
            {
                if (_affairsService.TrySearchItem(id, out var item))
                {
                    return View("Edit", item);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Affairs item)
        {
            if (item != null)
            {
                _affairsService.Update(item);
                return RedirectToAction("ViewToDo");
            }

            return NotFound();
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
