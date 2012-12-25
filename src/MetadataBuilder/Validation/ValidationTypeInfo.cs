using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using Tandil.MetadataBuilder.BaseTypes;

namespace Tandil.MetadataBuilder.Validation
{
	public class ValidationTypeInfo : GenericsTypeInfo<ValidationPropertyInfo>
	{
		public ValidationTypeInfo(Type modelType) : base(modelType)
		{
			Validators = new Collection<Func<ModelMetadata, ControllerContext, ModelValidator>>();
		}

		public ICollection<Func<ModelMetadata, ControllerContext, ModelValidator>> Validators { get; private set; }

		protected override ValidationPropertyInfo CreatePropertyInfo(string propertyName)
		{
			return new ValidationPropertyInfo(this, propertyName);
		}
	}
}