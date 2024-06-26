﻿using ToDoList.Core.Models.Errors;
using ToDoList.Core.Models.Users;
using ToDoList.Core.Service;
using ToDoList.Infrastructure.Authentication.Model;
using ToDoListMVC.Models;

namespace ToDoList.Infrastructure.Authentication
{
    public class UserModelValidator
    {
        private readonly UserService _userService;

        private readonly UserValidator _userValidator;

        public UserModelValidator(UserService userService, UserValidator userValidator)
        {
            _userService = userService;
            _userValidator = userValidator;
        }

        public IEnumerable<ErrorModel> Validate(UserModel userModel)
        {
            if (!_userValidator.ValidateUsername(userModel.Username, out var validUsernameMessage))
            {
                yield return new ErrorModel(AccessKeyErrorConstant.UsernameKey, validUsernameMessage);
            }

            if (!_userValidator.ValidatePassword(userModel.Password, out var validPasswordMessage))
            {
                yield return new ErrorModel(AccessKeyErrorConstant.PasswordKey, validPasswordMessage);
            }

            if (!_userService.IsFreeUsername(userModel.Username))
            {
                yield return new ErrorModel(AccessKeyErrorConstant.UsernameKey, ErrorMessage.MessageUsernameIsTaken);
            }

            yield break;
        }
    }
}
