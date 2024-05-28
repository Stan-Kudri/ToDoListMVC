using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ToDoList.Core.Authentication;
using ToDoList.Core.Service;
using ToDoList.Infrastructure.Authentication;
using ToDoList.Infrastructure.Authentication.Model;
using ToDoList.Infrastructure.Extension;
using ToDoListMVC.Extension;
using ToDoListMVC.Models;

namespace ToDoListMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenHelper;
        private readonly RefreshTokenService _refreshTokenService;

        public AuthenticationController(UserService userService, TokenService tokenHelper, RefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _refreshTokenService = refreshTokenService;
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
            if (!_userService.TryGetUserData(userModel, out var user))
            {
                ModelState.AddModelError(AccessKeyErrorConstant.EmptyKey, "The login or password was entered incorrectly.");
            }

            HttpContext.RemoveAllToken();

            if (ModelState.Any(e => e.Value?.ValidationState == ModelValidationState.Invalid))
            {
                return View("SignIn", userModel);
            }

            var token = _tokenHelper.GenerateTokenJWT(user);
            var refreshToken = _tokenHelper.GenerateRefreshToken(user.Id);

            _refreshTokenService.UppdataRefreshToken(refreshToken);

            HttpContext.AppendToken(token);
            HttpContext.AppendRefreshToken(refreshToken.Token);

            return RedirectToAction("ViewToDo", "ToDoList");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateAccount(UserModel userModel)
        {
            ModelState.ValidateModelUserRegistration(userModel);

            if (!_userService.IsFreeUsername(userModel.Username))
            {
                ModelState.AddModelError(AccessKeyErrorConstant.UsernameKey, "This username is taken.");
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
            if (HttpContext.Request.Cookies.TryGetValue(LoginConst.GetRefreshTokenKey, out var refreshTokenCookies) && _tokenHelper.UserId != null)
            {
                _refreshTokenService.Remove(refreshTokenCookies, (Guid)_tokenHelper.UserId);
                HttpContext.RemoveToken();
                HttpContext.RemoveRefreshToken();
                _tokenHelper.UserId = null;
            }

            return View("SignIn");
        }
    }
}
