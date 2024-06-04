using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ToDoList.Core.Authentication;
using ToDoList.Core.Models.Errors;
using ToDoList.Core.Service;
using ToDoList.Infrastructure.Authentication;
using ToDoList.Infrastructure.Authentication.Model;
using ToDoList.Infrastructure.Authentication.Tokens;
using ToDoList.Infrastructure.Extension;
using ToDoListMVC.Models;

namespace ToDoListMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenHelper;
        private readonly UserVerificator _userVerificator;
        private readonly CookieSettingService _cookieSettingService;

        public AuthenticationController(
            UserService userService,
            TokenService tokenHelper,
            UserVerificator userVerificator,
            CookieSettingService cookieSettingService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userVerificator = userVerificator;
            _cookieSettingService = cookieSettingService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignIn(UserModel userModel)
            => _tokenHelper.UserId is not null
                ? RedirectToAction("HomePage", "Home")
                : View();

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Registration(UserModel userModel)
            => _tokenHelper.UserId is not null
                ? RedirectToAction("HomePage", "Home")
                : View();

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ClearFieldRegistration()
        {
            ModelState.Clear();
            return View("Registration");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ClearFieldSignIn()
        {
            ModelState.Clear();
            return View("SignIn");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserModel userModel)
        {
            HttpContext.RemoveAllTokens();

            if (!_userService.TryGetUserData(userModel, out var user) || user == null)
            {
                ModelState.AddModelError(AccessKeyErrorConstant.EmptyKey, ErrorMessage.MessageInvalidUser);
                return View("SignIn", userModel);
            }

            _cookieSettingService.SetTokens(HttpContext, user);

            return RedirectToAction("ViewToDo", "ToDoList");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateAccount(UserModel userModel)
        {
            foreach (var error in _userVerificator.ValidateModelUserRegistration(userModel))
            {
                ModelState.AddModelError(error.AccsesKey, error.Message);
            }

            if (ModelState.Any(e => e.Value?.ValidationState == ModelValidationState.Invalid))
            {
                return View("Registration", userModel);
            }

            _userService.Add(userModel.ToUser());
            return View("SignIn");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Output()
        {
            if (HttpContext.Request.Cookies.TryGetValue(TokensConst.GetRefreshTokenKey, out var refreshToken) && _tokenHelper.UserId != null)
            {
                _cookieSettingService.RemoveTokens(HttpContext, refreshToken);
            }

            return View("SignIn");
        }
    }
}
