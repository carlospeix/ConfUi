using System;
using System.Linq;

namespace Tandil.MetadataBuilder.PatternAppliers
{
	public class ReferencePatternApplier : BasePatternApplier
	{
		public ReferencePatternApplier()
		{
			ExcludeReferenceTypes = new Type[] {};
		}

		public override void ModelRegistered(Type modelType)
		{
			foreach (var propertyInfo in modelType.GetProperties())
			{
				if (!ConfigurationHolder.MetadataMappings.Contains(propertyInfo.PropertyType))
					continue;
				if (IsExcludedReferenceType(propertyInfo.PropertyType))
					continue;
				var propertyRegistrar = BuildPropertyRegistrar(propertyInfo);
				propertyRegistrar.Reference();
			}
		}

		public Type[] ExcludeReferenceTypes { get; set; }

		private bool IsExcludedReferenceType(Type modelType)
		{
			return ExcludeReferenceTypes.Any(excludedType => modelType.Equals(excludedType));
		}
	}
}