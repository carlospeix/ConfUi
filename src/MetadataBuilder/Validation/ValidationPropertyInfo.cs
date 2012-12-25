using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using Tandil.MetadataBuilder.BaseTypes;

namespace Tandil.MetadataBuilder.Validation
{
    public class ValidationPropertyInfo : GenericsPropertyInfo<ValidationTypeInfo>
    {
        public ValidationPropertyInfo(ValidationTypeInfo typeInfo, string propertyName)
            : base(typeInfo, propertyName)
        {
            Validators = new Collection<Func<ModelMetadata, ControllerContext, ModelValidator>>();
        }

        public ICollection<Func<ModelMetadata, ControllerContext, ModelValidator>> Validators { get; private set; }
    }
}