using System;
using Tandil.MetadataBuilder.BaseTypes;

namespace Tandil.MetadataBuilder.Metadata
{
	public class ModelMetadataMappings : GenericsMappings<ModelMetadataTypeInfo>
	{
		protected override ModelMetadataTypeInfo CreateTypeInfo(Type type)
		{
			return new ModelMetadataTypeInfo(type);
		}

		public GenericsModelMetadata Apply(GenericsModelMetadata metadata)
		{
			var type = metadata.ContainerType ?? metadata.ModelType;

			if (Contains(type))
			{
				// Type-level modifiers
				var typeInfo = this[type];

				foreach (var modifier in typeInfo.Modifiers)
					modifier(metadata);

				// Property-level modifiers
				if (!String.IsNullOrWhiteSpace(metadata.PropertyName))
					foreach (var modifier in typeInfo[metadata.PropertyName].Modifiers)
						modifier(metadata);
			}

			return metadata;
		}
	}
}