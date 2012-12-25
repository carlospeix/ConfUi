using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Tandil.MetadataBuilder
{
	public interface IPropertyRegistrar<TModel, TProperty>
	{
		// Traverse up
		ITypeRegistrar<TModelTraverse> ForType<TModelTraverse>();

		// Traverse sideways
		IPropertyRegistrar<TModel, TPropertyTraverse> ForProperty<TPropertyTraverse>(
			Expression<Func<TModel, TPropertyTraverse>> expression);
		IPropertyRegistrar<TModel, TPropertyTraverse> ForProperty<TPropertyTraverse>(
			Expression<Func<TModel, TPropertyTraverse>> expression,
			Action<IPropertyRegistrar<TModel, TPropertyTraverse>> action);

		Type ModelType { get; }
		Type PropertyType { get; }

		// Metadata
		IPropertyRegistrar<TModel, TProperty> ConvertEmptyStringToNull(bool convertEmptyStringToNull);
		IPropertyRegistrar<TModel, TProperty> DataTypeName(string dataTypeName);
		IPropertyRegistrar<TModel, TProperty> Description(string description);
		IPropertyRegistrar<TModel, TProperty> DisplayFormatString(string displayFormatString);
		IPropertyRegistrar<TModel, TProperty> DisplayName(string displayName);
		IPropertyRegistrar<TModel, TProperty> EditFormatString(string editFormatString);
		IPropertyRegistrar<TModel, TProperty> HideSurroundingHtml(bool hideSurroundingHtml);
		IPropertyRegistrar<TModel, TProperty> NullDisplayText(string nullDisplayText);
		IPropertyRegistrar<TModel, TProperty> ShortDisplayName(string shortDisplayName);
		IPropertyRegistrar<TModel, TProperty> ShowForDisplay(bool showForDisplay);
		IPropertyRegistrar<TModel, TProperty> ShowForEdit(bool showForEdit);
		IPropertyRegistrar<TModel, TProperty> SimpleDisplayText(string simpleDisplayText);
		IPropertyRegistrar<TModel, TProperty> SkipRequestValidation(bool skip = true);
		IPropertyRegistrar<TModel, TProperty> TemplateHint(string templateHint);
		IPropertyRegistrar<TModel, TProperty> Watermark(string watermark);

		IPropertyRegistrar<TModel, TProperty> Reference();

		IPropertyRegistrar<TModel, TProperty> HiddenInput(bool displayValue = true);
		IPropertyRegistrar<TModel, TProperty> UiHint(string uiHint);
		IPropertyRegistrar<TModel, TProperty> DataType(DataType dataType /*, string errorMessage*/);
		IPropertyRegistrar<TModel, TProperty> Editable(bool allowEdit, bool allowInitialValue);
		IPropertyRegistrar<TModel, TProperty> ReadOnly(bool readOnly = true);
		IPropertyRegistrar<TModel, TProperty> Searchable(bool searchable = false);

		IPropertyRegistrar<TModel, TProperty> DisplayFormat(string nullDisplayText, string dataFormatString,
															bool convertEmptyStringToNull, bool applyFormatInEditMode,
															bool htmlEncode);
		IPropertyRegistrar<TModel, TProperty> ScaffoldColumn(bool scaffoldColumn);

		// Validators
		IPropertyRegistrar<TModel, TProperty> Required(string errorMessage = null);
		IPropertyRegistrar<TModel, TProperty> Range<T>(T minimum, T maximum, string errorMessage = null);
		IPropertyRegistrar<TModel, TProperty> RegularExpression(string pattern, string errorMessage = null);
		IPropertyRegistrar<TModel, TProperty> StringLength(int maximum = Int32.MaxValue, int minimum = 0, string errorMessage = null);
		IPropertyRegistrar<TModel, TProperty> Validate(Func<ModelMetadata, ControllerContext, ModelValidator> validatorFactory);
		IPropertyRegistrar<TModel, TProperty> EnumDataType(Type enumType, string errorMessage = null);
	}
}