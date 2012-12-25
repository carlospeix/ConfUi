using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Tandil.MetadataBuilder.Metadata
{
	public class GenericsModelMetadataProvider : ModelMetadataProvider
	{
		readonly ModelMetadataProvider _delegatingProvider;
		readonly ModelMetadataMappings _mappings;

		public GenericsModelMetadataProvider(ModelMetadataProvider delegatingProvider, ModelMetadataMappings mappings = null)
		{
			_delegatingProvider = delegatingProvider;
			_mappings = mappings ?? ConfigurationHolder.MetadataMappings;
		}

		public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
		{
			if (_mappings.Contains(modelType))
				return GetMetadataForTypeImpl(modelAccessor, modelType);

			return _delegatingProvider.GetMetadataForType(modelAccessor, modelType);
		}

		private GenericsModelMetadata GetMetadataForTypeImpl(Func<object> modelAccessor, Type modelType)
		{
			return _mappings.Apply(new GenericsModelMetadata(this, modelAccessor, modelType));
		}

		public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
		{
			if (_mappings.Contains(containerType))
				return GetMetadataForPropertiesImpl(container, containerType);

			return _delegatingProvider.GetMetadataForProperties(container, containerType);
		}

		private IEnumerable<ModelMetadata> GetMetadataForPropertiesImpl(object container, Type containerType)
		{
			// TODO: Armar sooprte para propiedades y fields publicos
			foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(containerType))
			{
				var modelAccessor = GetPropertyValueAccessor(container, property);
				yield return GetMetadataForPropertyImpl(modelAccessor, containerType, property);
			}
		}

		public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
		{
			if (containerType == null)
				throw new ArgumentNullException("containerType");

			if (_mappings.Contains(containerType))
			{
				if (String.IsNullOrEmpty(propertyName))
					throw new ArgumentException("Value cannot be null or empty.", "propertyName");

				// TODO: Armar sooprte para propiedades y fields publicos
				var property = TypeDescriptor.GetProperties(containerType).Find(propertyName, true);
				if (property == null)
					throw new ArgumentException(
						String.Format("The property {0}.{1} could not be found.", containerType.FullName, propertyName));

				return GetMetadataForPropertyImpl(modelAccessor, containerType, property);
			}

			return _delegatingProvider.GetMetadataForProperty(modelAccessor, containerType, propertyName);
		}

		private ModelMetadata GetMetadataForPropertyImpl(Func<object> modelAccessor, Type containerType, PropertyDescriptor propertyDescriptor)
		{
			var metadata = _mappings.Apply(
				new GenericsModelMetadata(this, containerType, modelAccessor, propertyDescriptor.PropertyType, propertyDescriptor.Name));
			
			if (metadata.ReferenceType != null)
				metadata.AdditionalValues.Add("selectList", BuildSelectList(metadata));

			return metadata;
		}

		private SelectList BuildSelectList(GenericsModelMetadata metadata)
		{
			var provider = ConfigurationHolder.DomainAccessorAccessor.Invoke(metadata.ReferenceType);
			if (provider == null)
				throw new InvalidOperationException(String.Format("The domain provider for the type {0} is invalid.", metadata.ReferenceType));

			var referenceMetadata = GetMetadataForTypeImpl(() => null, metadata.ReferenceType);
			if (referenceMetadata.IdMember == null)
			{
				throw new InvalidOperationException(String.Format(
						"{0} is configured as a reference type but it hasn't an Id property. " +
						"Consider configuring the Id property or excluding the type via the " +
						"ExcludeReferenceTypes property in the ReferencePatternApplier.",
						metadata.ReferenceType));
			}

			var list = provider.GetList();

			var selectList = list.Select(
				referenceValue => new SelectListItem
				                  	{
				                  		Value = GetIdValue(referenceMetadata, referenceValue).ToString(),
				                  		Text = GetDescriptionValue(referenceMetadata, referenceValue)
				                  	}).ToList();

			// TODO: probar si podemos usar esta sobrecarga (viendo el Equals)
			// return new SelectList(selectList, GetIdValue(metadata, metadata.Model));
			return new SelectList(selectList, "Value", "Text", GetIdValue(referenceMetadata, metadata.Model));
		}

		private static object GetIdValue(GenericsModelMetadata metadata, object model)
		{
			if (model == null || metadata.IdMember == null)
				return null;

			if (metadata.IdMember.MemberType == MemberTypes.Property)
				return ((PropertyInfo)metadata.IdMember).GetValue(model, new object[] {});

			if (metadata.IdMember.MemberType == MemberTypes.Field)
				return ((FieldInfo)metadata.IdMember).GetValue(model);

			return null;
		}

		private static string GetDescriptionValue(GenericsModelMetadata metadata, object model)
		{
			if (model != null)
				return model.ToString();

			return null;
		}

		private static Func<object> GetPropertyValueAccessor(object container, PropertyDescriptor property)
		{
			if (container == null)
				return null;

			return () => property.GetValue(container);
		}
	}
}