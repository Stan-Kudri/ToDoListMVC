using ToDoList.Infrastructure.Authentication.Model;

namespace ToDoList.Core.Models.Errors
{
    public class ErrorModel
    {
        public ErrorModel(string accsesKey, string message)
        {
            AccsesKey = accsesKey;
            Message = message;
        }

        public string AccsesKey { get; set; } = AccessKeyErrorConstant.EmptyKey;

        public string Message { get; set; } = string.Empty;
    }
}
