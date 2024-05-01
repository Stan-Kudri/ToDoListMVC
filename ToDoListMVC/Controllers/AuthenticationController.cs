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

        public AuthenticationController(IServiceProvider service)
        {
            _userService = service.GetRequiredService<UserService>();
            _tokenHelper = service.GetRequiredService<JwtTokenHelper>();
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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignIn(UserModel userModel)
        {
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

                HttpContext.Response.Cookies.Append("JWTBearer", token.ToString());

                return RedirectToAction("HomePage", "Home");
            }
        }

        [AllowAnonymous]
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

            _userService.Add(userModel.ToUser());
            return RedirectToAction("ViewToDo", "ToDoList");
        }
    }
}
