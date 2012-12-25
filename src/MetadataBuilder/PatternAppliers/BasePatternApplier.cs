using System;
using System.Reflection;
using Tandil.MetadataBuilder.Registrars;

namespace Tandil.MetadataBuilder.PatternAppliers
{
	public abstract class BasePatternApplier : IPatternApplier
	{
		public abstract void ModelRegistered(Type modelType);

		protected TypeRegistrar BuildTypeRegistrar(Type modelType)
		{
			return new TypeRegistrar(modelType);
		}

		protected PropertyRegistrar BuildPropertyRegistrar(Type modelType, Type propertyType, string propertyName)
		{
			return new PropertyRegistrar(modelType, propertyType, propertyName);
		}

		protected PropertyRegistrar BuildPropertyRegistrar(PropertyInfo propertyInfo)
		{
			return BuildPropertyRegistrar(propertyInfo.ReflectedType, propertyInfo.PropertyType, propertyInfo.Name);
		}
	}
}