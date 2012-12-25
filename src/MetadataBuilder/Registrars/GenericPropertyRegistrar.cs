using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

// http://bradwilson.typepad.com/blog/2009/10/aspnet-mvc-2-templates-part-2-modelmetadata.html

namespace Tandil.MetadataBuilder.Registrars
{
	public class PropertyRegistrar<TModel, TProperty> : IPropertyRegistrar<TModel, TProperty>
	{
		private readonly PropertyRegistrar _registrar;

		internal PropertyRegistrar(string propertyName)
		{
			_registrar = new PropertyRegistrar(typeof(TModel), typeof(TProperty), propertyName);
		}

		public ITypeRegistrar<TModelTraverse> ForType<TModelTraverse>()
		{
			return EnsureTypeIsKnownAndGetTypeRegistrar<TModelTraverse>();
		}

		public IPropertyRegistrar<TModel, TPropertyTraverse> ForProperty<TPropertyTraverse>(Expression<Func<TModel, TPropertyTraverse>> expression)
		{
			var memberInfo = TypeExtensions.DecodeMemberAccessExpression(expression);
			return new PropertyRegistrar<TModel, TPropertyTraverse>(memberInfo.Name);
		}

		public IPropertyRegistrar<TModel, TPropertyTraverse> ForProperty<TPropertyTraverse>(Expression<Func<TModel, TPropertyTraverse>> expression,
			Action<IPropertyRegistrar<TModel, TPropertyTraverse>> action)
		{
			var memberInfo = TypeExtensions.DecodeMemberAccessExpression(expression);
			var propertyRegistrar = new PropertyRegistrar<TModel, TPropertyTraverse>(memberInfo.Name);
			action(propertyRegistrar);
			return propertyRegistrar;
		}

		public Type ModelType
		{
			get { return _registrar.ModelType; }
		}

		public Type PropertyType
		{
			get { return _registrar.PropertyType; }
		}

		#region Metadata
		public IPropertyRegistrar<TModel, TProperty> ConvertEmptyStringToNull(bool convertEmptyStringToNull)
		{
			_registrar.ConvertEmptyStringToNull(convertEmptyStringToNull);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> DataTypeName(string dataTypeName)
		{
			_registrar.DataTypeName(dataTypeName);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> Description(string description)
		{
			_registrar.Description(description);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> DisplayFormatString(string displayFormatString)
		{
			_registrar.DisplayFormatString(displayFormatString);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> DisplayName(string displayName)
		{
			_registrar.DisplayName(displayName);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> EditFormatString(string editFormatString)
		{
			_registrar.EditFormatString(editFormatString);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> HideSurroundingHtml(bool hideSurroundingHtml)
		{
			_registrar.HideSurroundingHtml(hideSurroundingHtml);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> NullDisplayText(string nullDisplayText)
		{
			_registrar.NullDisplayText(nullDisplayText);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> ShortDisplayName(string shortDisplayName)
		{
			_registrar.ShortDisplayName(shortDisplayName);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> ShowForDisplay(bool showForDisplay)
		{
			_registrar.ShowForDisplay(showForDisplay);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> ShowForEdit(bool showForEdit)
		{
			_registrar.ShowForEdit(showForEdit);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> SimpleDisplayText(string simpleDisplayText)
		{
			_registrar.SimpleDisplayText(simpleDisplayText);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> SkipRequestValidation(bool skip = true)
		{
			_registrar.SkipRequestValidation(skip);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> TemplateHint(string templateHint)
		{
			_registrar.TemplateHint(templateHint);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> Watermark(string watermark)
		{
			_registrar.Watermark(watermark);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> Reference()
		{
			_registrar.Reference();
			return this;
		}
		#endregion

		#region Extended metadata
		public IPropertyRegistrar<TModel, TProperty> HiddenInput(bool displayValue = true)
		{
			_registrar.HiddenInput(displayValue);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> UiHint(string uiHint)
		{
			_registrar.UiHint(uiHint);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> DataType(DataType dataType)
		{
			_registrar.DataType(dataType);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> Editable(bool allowEdit, bool allowInitialValue)
		{
			_registrar.Editable(allowEdit, allowInitialValue);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> ReadOnly(bool readOnly = true)
		{
			_registrar.ReadOnly(readOnly);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> Searchable(bool searchable = false)
		{
			_registrar.Searchable(searchable);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> DisplayFormat(string nullDisplayText, string dataFormatString, bool convertEmptyStringToNull, bool applyFormatInEditMode, bool htmlEncode)
		{
			_registrar.DisplayFormat(nullDisplayText, dataFormatString, convertEmptyStringToNull, applyFormatInEditMode, htmlEncode);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> ScaffoldColumn(bool scaffoldColumn)
		{
			_registrar.ScaffoldColumn(scaffoldColumn);
			return this;
		}
		#endregion

		#region Validators
		public IPropertyRegistrar<TModel, TProperty> Required(string errorMessage = null)
		{
			_registrar.Required(errorMessage);
			return this;
		}

		//public IPropertyRegistrar<TModel, TProperty> ReferenceRequired(string errorMessage = null)
		//{
		//    _registrar.ReferenceRequired(errorMessage);
		//    return this;
		//}

		public IPropertyRegistrar<TModel, TProperty> Range<T>(T minimum, T maximum, string errorMessage = null)
		{
			_registrar.Range(minimum, maximum, errorMessage);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> RegularExpression(string pattern, string errorMessage = null)
		{
			_registrar.RegularExpression(pattern, errorMessage);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> StringLength(int maximum = Int32.MaxValue, int minimum = 0, string errorMessage = null)
		{
			_registrar.StringLength(maximum, minimum, errorMessage);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> Validate(Func<ModelMetadata, ControllerContext, ModelValidator> validatorFactory)
		{
			_registrar.Validate(validatorFactory);
			return this;
		}

		public IPropertyRegistrar<TModel, TProperty> EnumDataType(Type enumType, string errorMessage = null)
		{
			_registrar.EnumDataType(enumType, errorMessage);
			return this;
		}
		#endregion

		private static ITypeRegistrar<TModelTraverse> EnsureTypeIsKnownAndGetTypeRegistrar<TModelTraverse>()
		{
			return ConfigurationHolder.EnsureTypeIsKnownAndGetTypeRegistrar<TModelTraverse>();
		}
	}
}