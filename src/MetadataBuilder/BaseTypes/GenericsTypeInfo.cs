using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Tandil.MetadataBuilder.BaseTypes
{
	public abstract class GenericsTypeInfo    // Broken in two to prevent cyclical template definitions
	{
		protected GenericsTypeInfo(Type modelType)
		{
			ModelType = modelType;
		}

		public Type ModelType { get; private set; }
	}

	public abstract class GenericsTypeInfo<TPropertyInfo> : GenericsTypeInfo
	{
		private readonly ConcurrentDictionary<string, TPropertyInfo> _properties =
			new ConcurrentDictionary<string, TPropertyInfo>();

		protected GenericsTypeInfo(Type modelType) : base(modelType) { }

		public TPropertyInfo this[string propertyName]
		{
			get { return _properties.GetOrAdd(propertyName, CreatePropertyInfo); }
		}

		protected abstract TPropertyInfo CreatePropertyInfo(string propertyName);

		public IEnumerable<TPropertyInfo> Properties
		{
			get { return _properties.Values; }
		}
	}
}