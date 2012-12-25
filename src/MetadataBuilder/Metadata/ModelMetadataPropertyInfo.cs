using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tandil.MetadataBuilder.BaseTypes;

namespace Tandil.MetadataBuilder.Metadata
{
	public class ModelMetadataPropertyInfo : GenericsPropertyInfo<ModelMetadataTypeInfo>
    {
		public ModelMetadataPropertyInfo(ModelMetadataTypeInfo typeInfo, string propertyName) : base(typeInfo, propertyName)
		{
			Modifiers = new Collection<Action<GenericsModelMetadata>>();
		}

		public ICollection<Action<GenericsModelMetadata>> Modifiers { get; private set; }
    }
}