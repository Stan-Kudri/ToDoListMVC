using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Authentication;
using ToDoList.Core.Extension;
using ToDoList.Core.Models;
using ToDoList.Core.Models.Users;
using ToDoList.Core.Service;
using ToDoListMVC.Models;

namespace ToDoListMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly static UserValidator _userValidator = new UserValidator();

        private readonly UserService _userService;
        private readonly TokenService _tokenHelper;
        private readonly RefreshTokenService _refreshTokenService;
        private readonly AccessToken _accessToken;

        public AuthenticationController(UserService userService, TokenService tokenHelper, RefreshTokenService refreshTokenService, AccessToken accessToken)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _refreshTokenService = refreshTokenService;
            _accessToken = accessToken;
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
            if (_tokenHelper.UserId is not null)
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
                var refreshToken = _tokenHelper.GenerateRefreshToken(user);
                var cookiyOptionRefreshToken = new CookieOptions { HttpOnly = true };

                _refreshTokenService.UppdataRefreshToken(refreshToken);

                _accessToken.RefreshToken = refreshToken;
                _accessToken.Token = token;

                HttpContext.Response.Cookies.Append(LoginConst.GetTokenKey, token, cookiyOptionRefreshToken);

                return RedirectToAction("ViewToDo", "ToDoList");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Registration(UserModel userModel)
        {
            if (_tokenHelper.UserId is not null)
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
            return RedirectToAction("SignIn", "Authentication");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Output()
        {
            HttpContext.Response.Cookies.Delete(LoginConst.GetTokenKey);
            _refreshTokenService.Remove(_tokenHelper.UserId);
            _tokenHelper.UserId = null;
            _accessToken.RefreshToken = null;

            return RedirectToAction("HomePage", "Home");
        }
    }
}
