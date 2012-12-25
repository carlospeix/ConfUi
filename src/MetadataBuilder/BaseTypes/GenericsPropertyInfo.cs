using System;

namespace Tandil.MetadataBuilder.BaseTypes
{
	public class GenericsPropertyInfo<TTypeInfo> where TTypeInfo : GenericsTypeInfo
	{
		public GenericsPropertyInfo(TTypeInfo typeInfo, string propertyName)
		{
			ModelType = typeInfo.ModelType;
			PropertyName = propertyName;
		}

		public Type ModelType { get; private set; }
		public string PropertyName { get; private set; }
	}
}