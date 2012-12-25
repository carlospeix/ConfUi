using System;
using System.ComponentModel;
using System.Reflection;
using System.Web.Mvc;
using Tandil.MetadataBuilder.Metadata;

namespace Tandil.MetadataBuilder.ModelBinder
{
    public class ReferenceModelBinder : DefaultModelBinder
    {
    	private readonly Type _referenceType;

        public ReferenceModelBinder(Type referenceType)
        {
        	_referenceType = referenceType;
        }

		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if (IsReferenceBinding(bindingContext))
				return BindReference(controllerContext, bindingContext);

			return base.BindModel(controllerContext, bindingContext);
		}

        private static bool IsReferenceBinding(ModelBindingContext bindingContext)
        {
			return bindingContext.ModelMetadata.ContainerType != null &&
        	       bindingContext.ModelMetadata is GenericsModelMetadata;
        }

    	private object BindReference(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
			var metadata = bindingContext.ModelMetadata as GenericsModelMetadata;
			if (metadata == null)
				return null;

			var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (result == null || String.IsNullOrWhiteSpace(result.AttemptedValue))
			    return null;

			var provider = ConfigurationHolder.DomainAccessorAccessor.Invoke(_referenceType);
			if (provider == null)
				throw new InvalidOperationException(String.Format("The domain provider for the type {0} is invalid.", _referenceType));

			var converter = TypeDescriptor.GetConverter(GetIdMemberType(metadata));
			if (converter == null)
				return null;

    		var id = converter.ConvertFromString(result.AttemptedValue);
    		var reference = provider.Get(id);

    		return reference;
        }

    	private static Type GetIdMemberType(GenericsModelMetadata metadata)
    	{
			if (metadata.IdMember.MemberType == MemberTypes.Property)
				return ((PropertyInfo)metadata.IdMember).PropertyType;

			if (metadata.IdMember.MemberType == MemberTypes.Field)
				return ((FieldInfo)metadata.IdMember).FieldType;

    		throw new InvalidOperationException(
    			String.Format("IdMember {0} configured for model {1} should be field or property.", 
				              metadata.IdMember.Name, metadata.DisplayName));
    	}
	}
}