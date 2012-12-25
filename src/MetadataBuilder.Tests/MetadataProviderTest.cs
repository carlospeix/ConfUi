using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using NUnit.Framework;
using Tandil.MetadataBuilder;
using Tandil.MetadataBuilder.Metadata;

namespace MetadataBuilder.Tests
{
	[TestFixture]
	public class MetadataProviderTest
	{
		private DataAnnotationsModelMetadataProvider _damp;
		private GenericsModelMetadataProvider _gmp;
		private IModelRegistrar _reg;

		[SetUp]
		public void SetUp()
		{
			_damp = new DataAnnotationsModelMetadataProvider();
			_gmp = new GenericsModelMetadataProvider(_damp);
			_reg = ConfigurationHolder.GetRootRegistrar();
		}

		[TearDown]
		public void TearDown()
		{
			ConfigurationHolder.Reset();
		}

		[Test]
		public void DetailedUnmodifiedMetadataTest()
		{
			// Arrange
			var metadataDamp = _damp.GetMetadataForType(ModelAccessor(), typeof(ModelWithoutAttributes));
			var metadataGmp = _gmp.GetMetadataForType(ModelAccessor(), typeof(ModelWithoutAttributes));

			// Act

			// Assert
			CompareMetadata(metadataDamp, metadataGmp);
		}

		[Test]
		public void DetailedUnmodifiedButConfiguredMetadataTest()
		{
			// Arrange
			var metadataDamp = _damp.GetMetadataForType(ModelAccessor(), typeof(ModelWithoutAttributes));
			var metadataGmp = _gmp.GetMetadataForType(ModelAccessor(), typeof(ModelWithoutAttributes));

			// Act
			_reg.ForType<ModelWithoutAttributes>();

			// Assert
			CompareMetadata(metadataDamp, metadataGmp);
		}

		private static void CompareMetadata(ModelMetadata metadataDamp, ModelMetadata metadataGmp)
		{
			Assert.AreEqual(metadataDamp.AdditionalValues.Count, metadataGmp.AdditionalValues.Count);
			Assert.IsNull(metadataDamp.ContainerType); Assert.IsNull(metadataGmp.ContainerType);
			Assert.AreEqual(metadataDamp.ConvertEmptyStringToNull, metadataGmp.ConvertEmptyStringToNull);
			Assert.IsNull(metadataDamp.DataTypeName); Assert.IsNull(metadataGmp.DataTypeName);
			Assert.IsNull(metadataDamp.Description); Assert.IsNull(metadataGmp.Description);
			Assert.IsNull(metadataDamp.DisplayFormatString); Assert.IsNull(metadataGmp.DisplayFormatString);
			Assert.IsNull(metadataDamp.DisplayName); Assert.IsNull(metadataGmp.DisplayName);
			Assert.IsNull(metadataDamp.EditFormatString); Assert.IsNull(metadataGmp.EditFormatString);
			Assert.AreEqual(metadataDamp.GetDisplayName(), metadataGmp.GetDisplayName());
			Assert.AreEqual(metadataDamp.HideSurroundingHtml, metadataGmp.HideSurroundingHtml);
			Assert.AreEqual(metadataDamp.IsComplexType, metadataGmp.IsComplexType);
			Assert.AreEqual(metadataDamp.IsNullableValueType, metadataGmp.IsNullableValueType);
			Assert.AreEqual(metadataDamp.IsReadOnly, metadataGmp.IsReadOnly);
			Assert.AreEqual(metadataDamp.IsRequired, metadataGmp.IsRequired);
			Assert.IsNotNull(metadataDamp.Model); Assert.IsNotNull(metadataGmp.Model);
			Assert.AreEqual(metadataDamp.ModelType, metadataGmp.ModelType);
			Assert.IsNull(metadataDamp.NullDisplayText); Assert.IsNull(metadataGmp.NullDisplayText);
			Assert.AreEqual(metadataDamp.Order, metadataGmp.Order);
			Assert.IsNull(metadataDamp.PropertyName); Assert.IsNull(metadataGmp.PropertyName);
			Assert.AreEqual(metadataDamp.RequestValidationEnabled, metadataGmp.RequestValidationEnabled);
			Assert.IsNull(metadataDamp.ShortDisplayName); Assert.IsNull(metadataGmp.ShortDisplayName);
			Assert.AreEqual(metadataDamp.ShowForDisplay, metadataGmp.ShowForDisplay);
			Assert.AreEqual(metadataDamp.ShowForEdit, metadataGmp.ShowForEdit);
			Assert.AreEqual(metadataDamp.SimpleDisplayText, metadataGmp.SimpleDisplayText);
			Assert.IsNull(metadataDamp.TemplateHint); Assert.IsNull(metadataGmp.TemplateHint);
			Assert.IsNull(metadataDamp.Watermark); Assert.IsNull(metadataGmp.Watermark);

			ComparePropertyMetadata(metadataDamp.Properties, metadataGmp.Properties);
		}

		private static void ComparePropertyMetadata(IEnumerable<ModelMetadata> dampProperties, IEnumerable<ModelMetadata> gmpProperties)
		{
			Assert.AreEqual(2, dampProperties.Count());
			Assert.AreEqual(2, gmpProperties.Count());
		}

		public Func<object> ModelAccessor()
		{
			return () => new ModelWithoutAttributes();
		}
	}

	public class ModelWithoutAttributes
	{
		public string Code { get; set; }
		public string Description { get; set; }
	}
}
