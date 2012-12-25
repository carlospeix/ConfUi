using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tandil.MetadataBuilder;
using Tandil.MetadataBuilder.Metadata;
using Tandil.MetadataBuilder.Validation;

namespace MetadataBuilder.Tests
{
	public abstract class MetadataTests
	{
		protected static ModelMetadata GetTypeMetadata(Type modelType)
		{
			var provider = new GenericsModelMetadataProvider(null, ConfigurationHolder.MetadataMappings);
			return provider.GetMetadataForType(null, modelType);
		}

		protected static ModelMetadata GetPropertyMetadata(Type modelType, string propertyName)
		{
			var provider = new GenericsModelMetadataProvider(null, ConfigurationHolder.MetadataMappings);
			return provider.GetMetadataForProperty(null, modelType, propertyName);
		}

		protected static IEnumerable<ModelValidator> GetPropertyValidators(Type modelType, string propertyName)
		{
			var modelMetadata = GetPropertyMetadata(modelType, propertyName);
			var provider = new GenericsValidatorProvider(null, ConfigurationHolder.ValidationMappings);
			return provider.GetValidators(modelMetadata, null);
		}

		protected static IModelRegistrar BuildRegistration()
		{
			ConfigurationHolder.Reset();
			return ConfigurationHolder.GetRootRegistrar();
		}
	}
}