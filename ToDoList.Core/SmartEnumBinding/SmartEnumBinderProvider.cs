using Ardalis.SmartEnum;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ToDoList.Core.SmartEnumBinding
{
    public class SmartEnumBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            if (TypeUtil.IsDerived(context.Metadata.ModelType, typeof(SmartEnum<,>)))
            {
                return new BinderTypeModelBinder(typeof(SmartEnumModelBinder));
            }

            return null;
        }
    }
}
