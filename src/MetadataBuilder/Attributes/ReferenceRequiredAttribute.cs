using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Tandil.MetadataBuilder.Attributes
{
    public class ReferenceRequiredAttribute : ValidationAttribute, IClientValidatable
    {
        public bool Required { get; set; }

        public override bool IsValid(object value)
        {
            return !Required || value != null;
        }

        public override string FormatErrorMessage(string name)
        {
			return String.Format("El campo {0} no contiene una referencia válida.", name);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            if (!Required)
                yield break;

            yield return new ReferenceRequiredValidationRule(ErrorMessage, Required);
        }
    }

    public class ReferenceRequiredValidationRule : ModelClientValidationRule
    {
        public ReferenceRequiredValidationRule(string errorMessage, bool required)
        {
            if (required)
            {
                ErrorMessage = errorMessage;
                ValidationType = "required";
            }
        }
    }
}