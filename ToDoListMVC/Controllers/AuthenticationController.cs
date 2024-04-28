using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Models.Users;
using ToDoList.Core.Service;
using ToDoListMVC.Models;

namespace ToDoListMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly static UserValidator _userValidator = new UserValidator();

        private readonly UserService _userService;

        public AuthenticationController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult ClearFieldRegistration()
        {
            ModelState.Clear();
            return RedirectToAction("Registration", "Authentication");
        }

        [HttpGet]
        public IActionResult ClearFieldSignIn()
        {
            ModelState.Clear();
            return RedirectToAction("SignIn", "Authentication");
        }

        [HttpGet]
        public IActionResult SignIn(UserModel userModel)
        {
            if (!_userValidator.ValidFormatUsername(userModel.Username) || !_userValidator.ValidFormatPassword(userModel.Password))
            {
                ModelState.AddModelError("", "The data was entered incorrectly.");
                return View();
            }

            if (_userService.IsFreeUsername(userModel.Username) || !_userService.IsUserModelData(userModel))
            {
                ModelState.AddModelError("", "The login or password was entered incorrectly.");
            }

            if (ModelState.Any(e => e.Value?.ValidationState
                                    == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid))
            {
                return View();
            }

            return RedirectToAction("HomePage", "Home");
        }

        [HttpGet]
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

            if (ModelState.Any(e => e.Value?.ValidationState
                                    == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid))
            {
                return View();
            }

            var user = userModel.ToUser();
            _userService.Add(user);
            return RedirectToAction("ViewToDo", "ToDoList");
        }
    }
}
