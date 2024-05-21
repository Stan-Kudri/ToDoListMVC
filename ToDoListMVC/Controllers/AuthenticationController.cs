﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Authentication;
using ToDoList.Core.Extension;
using ToDoList.Core.Models.Users;
using ToDoList.Core.Service;
using ToDoListMVC.Extension;
using ToDoListMVC.Models;

namespace ToDoListMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly static UserValidator _userValidator = new UserValidator();

        private readonly UserService _userService;
        private readonly TokenService _tokenHelper;
        private readonly RefreshTokenService _refreshTokenService;

        public AuthenticationController(UserService userService, TokenService tokenHelper, RefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _refreshTokenService = refreshTokenService;
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

                _refreshTokenService.UppdataRefreshToken(refreshToken);

                HttpContext.AppendToken(token);
                HttpContext.AppendRefreshToken(refreshToken.Token);

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
            HttpContext.RemoveToken();
            HttpContext.RemoveRefreshToken();
            _refreshTokenService.Remove(_tokenHelper.UserId);
            _tokenHelper.UserId = null;

            return RedirectToAction("HomePage", "Home");
        }
    }
}
