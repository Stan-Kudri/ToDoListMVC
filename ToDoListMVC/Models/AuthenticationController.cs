using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Models.Users;
using ToDoList.Core.Service;

namespace ToDoListMVC.Models
{
    public class AuthenticationController : Controller
    {
        private readonly static UserValidator _userValidator = new UserValidator();

        private readonly UserService _userService;

        public AuthenticationController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Registration(UserModel userModel)
        {
            if (!_userValidator.ValidFormatUsername(userModel.Username, out var validUsernameMessage))
            {
                ModelState.AddModelError("username", validUsernameMessage);
            }

            if (!_userValidator.ValidFormatPassword(userModel.Password, out var validPasswordMessage))
            {
                ModelState.AddModelError("password", validPasswordMessage);
            }

            if (!_userService.IsFreeUsername(userModel.Username))
            {
                ModelState.AddModelError("username", "This username is taken ");
            }

            if (ModelState.Count() > 0)
            {
                return View();
            }

            var user = userModel.ToUser();
            _userService.Add(user);
            return RedirectToAction("ViewToDo", "ToDoList");
        }

        public IActionResult LogIn()
        {
            return View();
        }
    }
}
