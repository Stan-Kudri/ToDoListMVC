using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Controllers;
using ToDoList.Core.Models;
using ToDoList.Core.Models.Users.PersonalData;
using ToDoList.Core.Service;

namespace ToDoListMVC.Controllers
{
    [Authorize(Roles = "User")]
    public class PersonalDateController(UserService userService, ICurrentUserAccessor currentUser)
        : Controller
    {
        [HttpGet]
        public IActionResult PersonalDate()
            => View(userService.GetUserPersonalDate(currentUser.UserId));

        [HttpPost]
        public IActionResult Update(UserPersonalDataModel userPersonalData)
        {
            userService.UpdatePersonalData(userPersonalData);
            return RedirectToAction(HomeController.NameHomePage, HomeController.NameHomeController);
        }

        [HttpGet]
        public IActionResult Close()
            => RedirectToAction(ToDoListController.NameViewToDoPage, ToDoListController.NameToDoListController);
    }
}
