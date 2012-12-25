using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tandil.MetadataBuilder.BaseTypes;

namespace Tandil.MetadataBuilder.Validation
{
	public class ValidationMappings : GenericsMappings<ValidationTypeInfo>
	{
		protected override ValidationTypeInfo CreateTypeInfo(Type type)
		{
			return new ValidationTypeInfo(type);
		}

		public IEnumerable<ModelValidator> GetValidators(Type modelType, ModelMetadata metadata, ControllerContext context)
		{
			var typeInfo = this[modelType];
			var validatorFactories =
				String.IsNullOrWhiteSpace(metadata.PropertyName)
					? typeInfo.Validators                            // Type level validation
					: typeInfo[metadata.PropertyName].Validators;    // Property level validation

			foreach (var validatorFactory in validatorFactories)
				yield return validatorFactory(metadata, context);
		}
	}
}