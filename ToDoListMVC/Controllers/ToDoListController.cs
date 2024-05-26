﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly ToDoItemsService _affairsService;

        public ToDoListController(ILogger<ToDoListController> logger, ToDoItemsService caseService)
        {
            _logger = logger;
            _affairsService = caseService;
        }

        [HttpGet]
        public IActionResult ViewToDo() => View();


        [HttpPost]
        public IActionResult ViewToDo(ToDoItemsModel item)
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
            => id != null && _affairsService.TrySearchItem(id, out var item) && !string.IsNullOrEmpty(item.Description)
                ? View("Edit", item)
                : NoContent();

        [HttpPost]
        public IActionResult Edit(ToDoItems item)
        {
            if (item == null || string.IsNullOrEmpty(item.Description))
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

            return NoContent();
        }

        [HttpPost]
        public IActionResult ChangeDescription(Guid? id, string description)
        {
            if (id != null && !string.IsNullOrEmpty(description))
            {
                _affairsService.UpdateDescription(id, description);
            }

            return RedirectToAction("ViewToDo");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
