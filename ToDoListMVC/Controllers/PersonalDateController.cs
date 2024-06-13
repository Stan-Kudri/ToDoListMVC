using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        {
            return View(_userService.GetUserPersonalDate(_currentUser.UserId));
        }

        [HttpPost]
        public IActionResult Update(UserPersonalDataModel userPersonalData)
        {
            _userService.UpdatePersonalData(userPersonalData);
            return RedirectToAction("HomePage", "Home");
        }

        [HttpGet]
        public IActionResult Close()
            => RedirectToAction("ViewToDo", "ToDoList");
    }
}
