using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Authentication;
using ToDoList.Core.Models.Users;
using ToDoList.Core.Service;
using ToDoListMVC.Models;

namespace ToDoListMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly static UserValidator _userValidator = new UserValidator();

        private readonly UserService _userService;
        private readonly JwtTokenHelper _tokenHelper;

        public AuthenticationController(UserService userService, JwtTokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
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
            HttpContext.Response.Cookies.Delete(LoginConst.GetTokenKey);
            return RedirectToAction("SignIn", "Authentication");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignIn(UserModel userModel)
        {
            if (HttpContext.Request.Cookies.TryGetValue(LoginConst.GetTokenKey, out var usingToken) && _tokenHelper.IsUserIdGetByToken(usingToken))
            {
                return RedirectToAction("HomePage", "Home");
            }

            if (!_userValidator.ValidFormatUsername(userModel.Username) || !_userValidator.ValidFormatPassword(userModel.Password))
            {
                ModelState.AddModelError("", "The data was entered incorrectly.");
                return View();
            }

            if (!_userService.IsUserModelData(userModel, out var user))
            {
                ModelState.AddModelError("", "The login or password was entered incorrectly.");
            }

            if (ModelState.Any(e => e.Value?.ValidationState
                                    == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid))
            {
                return View(Results.Unauthorized());
            }
            else
            {
                var token = _tokenHelper.GenerateTokenJWT(user);

                HttpContext.Response.Cookies.Append(LoginConst.GetTokenKey, token);

                return RedirectToAction("ViewToDo", "ToDoList");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Registration(UserModel userModel)
        {
            if (HttpContext.Request.Cookies.TryGetValue(LoginConst.GetTokenKey, out var usingToken) && _tokenHelper.IsUserIdGetByToken(usingToken))
            {
                return RedirectToAction("HomePage", "Home");
            }

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

            _userService.Add(userModel.ToUser());
            return RedirectToAction("ViewToDo", "ToDoList");
        }
    }
}
