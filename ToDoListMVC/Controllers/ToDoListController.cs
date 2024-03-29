﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Core.Models;
using ToDoList.Core.Repository;
using ToDoList.Models;

namespace ToDoListMVC.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly ILogger<ToDoListController> _logger;
        private readonly AffairsService _affairsService;

        public ToDoListController(ILogger<ToDoListController> logger, AffairsService caseService)
        {
            _logger = logger;
            _affairsService = caseService;
        }

        [HttpGet]
        public IActionResult ViewToDo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ViewToDo(AffairsModel item)
        {
            var affairsModel = new Affairs(
                                            item.description,
                                            DateTime.Now,
                                            item.isCaseCompletion,
                                            item.isCaseCompletion == true ? DateTime.Now : null);

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
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
