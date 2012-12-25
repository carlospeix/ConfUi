using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tandil.MetadataBuilder.BaseTypes;

namespace Tandil.MetadataBuilder.Metadata
{
	public class ModelMetadataTypeInfo : GenericsTypeInfo<ModelMetadataPropertyInfo>
	{
		public ModelMetadataTypeInfo(Type modelType) : base(modelType)
		{
			Modifiers = new Collection<Action<GenericsModelMetadata>>();
		}

		public ICollection<Action<GenericsModelMetadata>> Modifiers { get; private set; }

		protected override ModelMetadataPropertyInfo CreatePropertyInfo(string propertyName)
		{
			return new ModelMetadataPropertyInfo(this, propertyName);
		}
	}
}