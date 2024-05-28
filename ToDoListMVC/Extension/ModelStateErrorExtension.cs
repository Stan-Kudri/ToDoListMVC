using Microsoft.AspNetCore.Mvc.ModelBinding;
using ToDoList.Core.Models.Users;
using ToDoList.Infrastructure.Authentication.Model;
using ToDoListMVC.Models;

namespace ToDoListMVC.Extension
{
    public static class ModelStateErrorExtension
    {
        private static UserValidator _userValidator = new UserValidator();

        public static ModelStateDictionary ValidateModelUserRegistration(this ModelStateDictionary modelState, UserModel userModel)
        {
            if (!_userValidator.ValidetUsername(userModel.Username, out var validUsernameMessage))
            {
                modelState.AddModelError(AccessKeyErrorConstant.UsernameKey, validUsernameMessage);
            }

            if (!_userValidator.ValidatePassword(userModel.Password, out var validPasswordMessage))
            {
                modelState.AddModelError(AccessKeyErrorConstant.PasswordKey, validPasswordMessage);
            }

            return modelState;
        }
    }
}
