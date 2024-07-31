using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Controllers;
using ToDoList.Core.Models;
using ToDoList.Core.Models.Users.PersonalData;
using ToDoList.Core.Service;

namespace ToDoListMVC.Controllers
{
    [Authorize(Roles = "User")]
    public class PersonalDateController : Controller
    {
        private readonly UserService _userService;
        private readonly ICurrentUserAccessor _currentUser;

        public PersonalDateController(UserService userService, ICurrentUserAccessor currentUser)
        {
            _userService = userService;
            _currentUser = currentUser;
        }

        [HttpGet]
        public IActionResult PersonalDate()
            => View(_userService.GetUserPersonalDate(_currentUser.UserId));

        [HttpPost]
        public IActionResult Update(UserPersonalDataModel userPersonalData)
        {
            _userService.UpdatePersonalData(userPersonalData);
            return RedirectToAction(HomeController.NameHomePage, HomeController.NameHomeController);
        }

        [HttpGet]
        public IActionResult Close()
            => RedirectToAction(ToDoListController.NameViewToDoPage, ToDoListController.NameToDoListController);
    }
}
