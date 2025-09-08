using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ToDoList.Core.Authentication;
using ToDoList.Core.Models.Errors;
using ToDoList.Core.Service;
using ToDoList.Infrastructure.Authentication;
using ToDoList.Infrastructure.Authentication.Model;
using ToDoList.Infrastructure.Extension;
using ToDoListMVC.Models;

namespace ToDoListMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private const string NameRazorPageSignIn = "SignIn";
        private const string NameRazorPageRegistration = "Registration";

        private readonly UserService _userService;
        private readonly TokenService _tokenHelper;
        private readonly UserModelValidator _userVerificator;
        private readonly ICookieSettingService _cookieSettingService;

        public AuthenticationController(
            UserService userService,
            TokenService tokenHelper,
            UserModelValidator userVerificator,
            ICookieSettingService cookieSettingService)
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
                ? RedirectToAction(ToDoListController.NameViewToDoPage, ToDoListController.NameToDoListController)
                : View();

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Registration(UserModel userModel)
            => _tokenHelper.UserId is not null
                ? RedirectToAction(ToDoListController.NameViewToDoPage, ToDoListController.NameToDoListController)
                : View();

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ClearFieldRegistration()
        {
            ModelState.Clear();
            return View(NameRazorPageRegistration);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ClearFieldSignIn()
        {
            ModelState.Clear();
            return View(NameRazorPageSignIn);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserModel userModel)
        {
            HttpContext.RemoveAllTokens();

            if (!_userService.TryGetUserData(userModel, out var user) || user == null)
            {
                ModelState.AddModelError(AccessKeyErrorConstant.EmptyKey, ErrorMessage.MessageInvalidUser);
                return View(NameRazorPageSignIn, userModel);
            }

            _cookieSettingService.SetTokens(HttpContext, user);

            return RedirectToAction(ToDoListController.NameViewToDoPage, ToDoListController.NameToDoListController);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateAccount(UserModel userModel)
        {
            foreach (var error in _userVerificator.Validate(userModel))
            {
                ModelState.AddModelError(error.AccsesKey, error.Message);
            }

            if (ModelState.Any(e => e.Value?.ValidationState == ModelValidationState.Invalid))
            {
                return View(NameRazorPageRegistration, userModel);
            }

            _userService.Add(userModel.ToUser());
            return View(NameRazorPageSignIn);
        }

        [Authorize]
        [HttpGet]
        public IActionResult SignOut()
        {
            if (HttpContext.Request.Cookies.TryGetValue(TokensConst.GetRefreshTokenKey, out var refreshToken) && _tokenHelper.UserId != null)
            {
                _cookieSettingService.RemoveTokens(HttpContext, refreshToken);
            }

            return View(NameRazorPageSignIn);
        }
    }
}
