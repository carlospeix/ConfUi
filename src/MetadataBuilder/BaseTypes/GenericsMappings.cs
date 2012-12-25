using System;
using System.Collections.Concurrent;

namespace Tandil.MetadataBuilder.BaseTypes
{
	public abstract class GenericsMappings<TTypeInfo> where TTypeInfo : GenericsTypeInfo
	{
		readonly ConcurrentDictionary<Type, TTypeInfo> _mappings = new ConcurrentDictionary<Type, TTypeInfo>();

		public TTypeInfo this[Type type]
		{
			get { return _mappings.GetOrAdd(type, CreateTypeInfo); }
		}

		protected ConcurrentDictionary<Type, TTypeInfo> Mappings
		{
			get { return _mappings; }
		}

		public bool Contains(Type type)
		{
			return _mappings.ContainsKey(type);
		}

		public void RegisterType(Type type)
		{
			_mappings.GetOrAdd(type, CreateTypeInfo);
		}

		protected abstract TTypeInfo CreateTypeInfo(Type type);
	}
}