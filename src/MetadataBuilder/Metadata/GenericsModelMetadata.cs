using System;
using System.Reflection;
using System.Web.Mvc;

namespace Tandil.MetadataBuilder.Metadata
{
	public class GenericsModelMetadata : ModelMetadata
	{
		internal GenericsModelMetadata(GenericsModelMetadataProvider provider, Func<object> modelAccessor, Type modelType)
			: base(provider, null /* containerType */, modelAccessor, modelType, null /* propertyName */)
		{
			InstanceValidator = model => { };
			InstanceDescription = model => model.ToString();
		}

		internal GenericsModelMetadata(GenericsModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
			: base(provider, containerType, modelAccessor, modelType, propertyName)
		{
			InstanceValidator = model => { };
			InstanceDescription = model => model.ToString();
		}

		public MemberInfo IdMember { get; set; }
		public Func<GenericsModelMetadata, string> SimpleDisplayTextCallback { get; set; }
		public Type ReferenceType { get; set; }
		public bool Searchable { get; set; }
		public MemberInfo InitialSortMember { get; set; }

		public Action<object> InstanceValidator { get; set; }
		public Func<object, string> InstanceDescription { get; set; }

		public Func<object> DomainAccessorAccessor()
		{
			return () => null;
		}

		protected override string GetSimpleDisplayText()
		{
			if (SimpleDisplayTextCallback != null)
				return SimpleDisplayTextCallback(this);

			return base.GetSimpleDisplayText();
		}
	}
}