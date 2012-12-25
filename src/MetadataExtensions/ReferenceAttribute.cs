using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MetadataExtensions
{
    public class ReferenceAttribute : ValidationAttribute, IClientValidatable
    {
        public ReferenceAttribute()
        {
            ErrorMessage = "Este campo debe ser completado";
        }

        public bool Required { get; set; }

        public override bool IsValid(object value)
        {
            return !Required || value != null;
        }

        public override string FormatErrorMessage(string name)
        {
            return "El campo " + name + " no contiene una referencia válida.";
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