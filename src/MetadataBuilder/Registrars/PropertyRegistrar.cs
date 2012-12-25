using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Tandil.MetadataBuilder.Metadata;
using Tandil.MetadataBuilder.ModelBinder;

// http://bradwilson.typepad.com/blog/2009/10/aspnet-mvc-2-templates-part-2-modelmetadata.html

namespace Tandil.MetadataBuilder.Registrars
{
	public class PropertyRegistrar
	{
		private readonly Type _modelType;
		private readonly Type _propertyType;
		private readonly string _propertyName;

		internal PropertyRegistrar()
		{
		}

		public PropertyRegistrar(Type modelType, Type propertyType, string propertyName)
		{
			_modelType = modelType;
			_propertyType = propertyType;
			_propertyName = propertyName;
		}

		public Type ModelType
		{
			get { return _modelType; }
		}

		public Type PropertyType
		{
			get { return _propertyType; }
		}

		private string PropertyName
		{
			get { return _propertyName; }
		}

		#region Metadata
		public void ConvertEmptyStringToNull(bool convertEmptyStringToNull)
		{
			Modifiers.Add(metadata => metadata.ConvertEmptyStringToNull = convertEmptyStringToNull);
		}

		public void DataTypeName(string dataTypeName)
		{
			Modifiers.Add(metadata => metadata.DataTypeName = dataTypeName);
		}

		public void Description(string description)
		{
			Modifiers.Add(metadata => metadata.Description = description);
		}

		public void DisplayFormatString(string displayFormatString)
		{
			Modifiers.Add(metadata => metadata.DisplayFormatString = displayFormatString);
		}

		public void DisplayName(string displayName)
		{
			Modifiers.Add(metadata => metadata.DisplayName = displayName);
		}

		public void EditFormatString(string editFormatString)
		{
			Modifiers.Add(metadata => metadata.EditFormatString = editFormatString);
		}

		public void HideSurroundingHtml(bool hideSurroundingHtml)
		{
			Modifiers.Add(metadata => metadata.HideSurroundingHtml = hideSurroundingHtml);
		}

		public void NullDisplayText(string nullDisplayText)
		{
			Modifiers.Add(metadata => metadata.NullDisplayText = nullDisplayText);
		}

		public void ShortDisplayName(string shortDisplayName)
		{
			Modifiers.Add(metadata => metadata.ShortDisplayName = shortDisplayName);
		}

		public void ShowForDisplay(bool showForDisplay)
		{
			Modifiers.Add(metadata => metadata.ShowForDisplay = showForDisplay);
		}

		public void ShowForEdit(bool showForEdit)
		{
			Modifiers.Add(metadata => metadata.ShowForEdit = showForEdit);
		}

		public void SimpleDisplayText(string simpleDisplayText)
		{
			Modifiers.Add(metadata => metadata.SimpleDisplayText = simpleDisplayText);
		}

		public void SkipRequestValidation(bool skip = true)
		{
			Modifiers.Add(metadata => metadata.RequestValidationEnabled = !skip);
		}

		public void TemplateHint(string templateHint)
		{
			Modifiers.Add(metadata => metadata.TemplateHint = templateHint);
		}

		public void Watermark(string watermark)
		{
			Modifiers.Add(metadata => metadata.Watermark = watermark);
		}

		public void Reference()
		{
			Modifiers.Add(metadata => metadata.ReferenceType = _propertyType);
			ConfigurationHolder.AddBinder(_propertyType, new ReferenceModelBinder(_propertyType));
		}
		#endregion

		#region Extended metadata
		public void HiddenInput(bool displayValue = true)
		{
			// Same behaviour as DataAnnotationsModelMetadataProvider
			Modifiers.Add(metadata =>
			{
				metadata.TemplateHint = "HiddenInput";
				metadata.HideSurroundingHtml = !displayValue;
			});
		}

		public void UiHint(string uiHint)
		{
			// Same behaviour as DataAnnotationsModelMetadataProvider
			Modifiers.Add(metadata => metadata.TemplateHint = uiHint);
		}

		public void DataType(DataType dataType)
		{
			// TODO: Same behaviour as DataAnnotationsModelMetadataProvider
			Modifiers.Add(metadata => metadata.DataTypeName = dataType.ToString());
		}

		public void Editable(bool allowEdit, bool allowInitialValue)
		{
			// Same behaviour as DataAnnotationsModelMetadataProvider
			Modifiers.Add(metadata => metadata.IsReadOnly = !allowEdit);
		}

		public void ReadOnly(bool readOnly = true)
		{
			// Same behaviour as DataAnnotationsModelMetadataProvider
			Modifiers.Add(metadata => metadata.IsReadOnly = readOnly);
		}

		public void Searchable(bool searchable = false)
		{
			// Same behaviour as DataAnnotationsModelMetadataProvider
			Modifiers.Add(metadata => metadata.Searchable = searchable);
		}

		public void DisplayFormat(string nullDisplayText, string dataFormatString, bool convertEmptyStringToNull, bool applyFormatInEditMode, bool htmlEncode)
		{
			// TODO: Same behaviour as DataAnnotationsModelMetadataProvider
			Modifiers.Add(metadata =>
			{
				metadata.NullDisplayText = nullDisplayText;
				metadata.DisplayFormatString = dataFormatString;
				metadata.ConvertEmptyStringToNull = convertEmptyStringToNull;
				if (applyFormatInEditMode)
					metadata.EditFormatString = dataFormatString;
			});
		}

		public void ScaffoldColumn(bool scaffoldColumn)
		{
			// Same behaviour as DataAnnotationsModelMetadataProvider
			Modifiers.Add(metadata => metadata.ShowForEdit = metadata.ShowForEdit = scaffoldColumn);
		}
		#endregion

		#region Validators
		public void Required(string errorMessage = null)
		{
			if (errorMessage == null)
				errorMessage = "Debe ingresar un valor para el campo {0}";
			Modifiers.Add(metadata => metadata.IsRequired = true);
			Validators.Add((metadata, context) => RequiredFactory(metadata, context, errorMessage));
		}

		//public void ReferenceRequired(string errorMessage = null)
		//{
		//    if (errorMessage == null)
		//        errorMessage = "Debe ingresar un valor para el campo {0}";
		//    Modifiers.Add(metadata => metadata.IsRequired = true);
		//    Validators.Add((metadata, context) => ReferenceRequiredFactory(metadata, context, errorMessage));
		//}

		public void Range<T>(T minimum, T maximum, string errorMessage = null)
		{
			if (errorMessage == null)
				errorMessage = "El campo {0} " + String.Format("admite el rango de valores [{0}-{1}].", minimum, maximum);
			Validators.Add((metadata, context) => RangeFactory(metadata, context, typeof(T), minimum.ToString(), maximum.ToString(), errorMessage));
		}

		public void RegularExpression(string pattern, string errorMessage = null)
		{
			Validators.Add((metadata, context) => RegexFactory(metadata, context, pattern, errorMessage));
		}

		public void StringLength(int maximum, int minimum = 0, string errorMessage = null)
		{
			if (errorMessage == null)
				if (minimum == 0)
					errorMessage = "El campo {0} " + String.Format("admite {0} caracteres como máximo.", maximum);
				else
					errorMessage = "El campo {0} " + String.Format("admite entre {0} y {1} caracteres.", minimum, maximum);
			Validators.Add((metadata, context) => StringLengthFactory(metadata, context, minimum, maximum, errorMessage));
		}

		public void Validate(Func<ModelMetadata, ControllerContext, ModelValidator> validatorFactory)
		{
			Validators.Add(validatorFactory);
		}

		public void EnumDataType(Type enumType, string errorMessage = null)
		{
			//Validators.Add((metadata, context) => EnumDataTypeFactory(metadata, context, enumType, errorMessage));
		}

		#region Helpers validators
		private static ModelValidator RangeFactory(ModelMetadata metadata, ControllerContext context, Type type, string minimum, string maximum, string errorMessage)
		{
			var attribute = new RangeAttribute(type, minimum, maximum);

			if (!String.IsNullOrWhiteSpace(errorMessage))
				attribute.ErrorMessage = errorMessage;

			return new RangeAttributeAdapter(metadata, context, attribute);
		}

		private static ModelValidator RegexFactory(ModelMetadata metadata, ControllerContext context, string pattern, string errorMessage)
		{
			var attribute = new RegularExpressionAttribute(pattern);

			if (!String.IsNullOrWhiteSpace(errorMessage))
				attribute.ErrorMessage = errorMessage;

			return new RegularExpressionAttributeAdapter(metadata, context, attribute);
		}

		private static ModelValidator RequiredFactory(ModelMetadata metadata, ControllerContext context, string errorMessage)
		{
			var attribute = new RequiredAttribute();

			if (!String.IsNullOrWhiteSpace(errorMessage))
				attribute.ErrorMessage = errorMessage;

			return new RequiredAttributeAdapter(metadata, context, attribute);
		}

		//private static ModelValidator ReferenceRequiredFactory(ModelMetadata metadata, ControllerContext context, string errorMessage)
		//{
		//    var attribute = new ReferenceRequiredAttribute();

		//    if (!String.IsNullOrWhiteSpace(errorMessage))
		//        attribute.ErrorMessage = errorMessage;

		//    return new ReferenceRequiredAdapter(metadata, context, attribute);
		//}

		private static ModelValidator StringLengthFactory(ModelMetadata metadata, ControllerContext context, int minimum, int maximum, string errorMessage)
		{
			var attribute = new StringLengthAttribute(maximum) { MinimumLength = minimum };

			if (!String.IsNullOrWhiteSpace(errorMessage))
				attribute.ErrorMessage = errorMessage;

			return new StringLengthAttributeAdapter(metadata, context, attribute);
		}
		#endregion

		#endregion

		private ICollection<Action<GenericsModelMetadata>> Modifiers
		{
			get { return ConfigurationHolder.MetadataMappings[ModelType][PropertyName].Modifiers; }
		}

		private ICollection<Func<ModelMetadata, ControllerContext, ModelValidator>> Validators
		{
			get { return ConfigurationHolder.ValidationMappings[ModelType][PropertyName].Validators; }
		}
	}
}