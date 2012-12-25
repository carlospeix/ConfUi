using System.Collections.Generic;
using System.Web.Mvc;

namespace Tandil.MetadataBuilder.Validation
{
	public class GenericsValidatorProvider : ModelValidatorProvider
	{
		readonly ModelValidatorProvider _delegatingProvider;
		readonly ValidationMappings _mappings;

		public GenericsValidatorProvider(ModelValidatorProvider delegatingProvider, ValidationMappings mappings = null)
		{
			_delegatingProvider = delegatingProvider;
			_mappings = mappings ?? ConfigurationHolder.ValidationMappings;
		}

		public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
		{
			var modelType = metadata.ContainerType ?? metadata.ModelType;

			if (_mappings.Contains(modelType))
				return _mappings.GetValidators(modelType, metadata, context);

			return _delegatingProvider.GetValidators(metadata, context);
		}
	}
}