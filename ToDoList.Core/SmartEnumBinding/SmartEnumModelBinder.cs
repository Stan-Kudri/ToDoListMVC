using Ardalis.SmartEnum;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;

namespace ToDoList.Core.SmartEnumBinding
{
    public class SmartEnumModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            var modelType = bindingContext.ModelMetadata.ModelType;

            if (!TypeUtil.IsDerived(modelType, typeof(SmartEnum<,>)))
            {
                throw new ArgumentException($"{modelType} is not a SmartEnum");
            }

            var propertyName = bindingContext.ModelName;
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(propertyName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(propertyName, valueProviderResult);

            string enumKeyName = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(enumKeyName))
            {
                return Task.CompletedTask;
            }

            // Create smart enum instance from enum key name by calling the FromName static method on the SmartEnum Class
            Type baseSmartEnumType = TypeUtil.GetTypeFromGenericType(modelType, typeof(SmartEnum<,>));
            foreach (var methodInfo in baseSmartEnumType.GetMethods().Where(methodInfo => methodInfo.Name == "FromName"))
            {
                ParameterInfo[] methodsParams = methodInfo.GetParameters();
                if (methodsParams.Length == 2)
                {
                    if (methodsParams[0].ParameterType == typeof(string) && methodsParams[1].ParameterType == typeof(bool))
                    {
                        var enumObj = methodInfo.Invoke(null, [enumKeyName, true]);
                        bindingContext.Result = ModelBindingResult.Success(enumObj);
                        return Task.CompletedTask;
                    }
                }
            }

            bindingContext.ModelState.TryAddModelError(propertyName, $"unable to call FromName on the SmartEnum of type {modelType}");
            return Task.CompletedTask;
        }
    }
}
