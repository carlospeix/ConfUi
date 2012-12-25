using System;
using System.Web.Mvc;
using NUnit.Framework;
using Tandil.MetadataBuilder;

namespace MetadataBuilder.Tests
{
	[TestFixture]
	public class PropertyMetadataTests : MetadataTests
	{
		IModelRegistrar _reg;

		[SetUp]
		public void SetUp()
		{
			_reg = BuildRegistration();
		}

		[Test]
		public void UnmodifiedMetadataForProperty()
		{
			_reg.ForType<Customer>();

			var metadata = GetPropertyMetadata(typeof(Customer), "FirstName");

			Assert.AreEqual(typeof(Customer), metadata.ContainerType);
			Assert.IsTrue(metadata.ConvertEmptyStringToNull);
			Assert.IsNull(metadata.DataTypeName);
			Assert.IsNull(metadata.Description);
			Assert.IsNull(metadata.DisplayFormatString);
			Assert.IsNull(metadata.DisplayName);
			Assert.IsNull(metadata.EditFormatString);
			Assert.AreEqual("FirstName", metadata.GetDisplayName());
			Assert.IsFalse(metadata.HideSurroundingHtml);
			Assert.IsFalse(metadata.IsComplexType);
			Assert.IsFalse(metadata.IsNullableValueType);
			Assert.IsFalse(metadata.IsReadOnly);
			Assert.IsFalse(metadata.IsRequired);
			Assert.AreEqual(typeof(String), metadata.ModelType);
			Assert.IsNull(metadata.NullDisplayText);
			Assert.AreEqual(ModelMetadata.DefaultOrder, metadata.Order);
			Assert.AreEqual("FirstName", metadata.PropertyName);
			Assert.IsNull(metadata.ShortDisplayName);
			Assert.IsTrue(metadata.ShowForDisplay);
			Assert.IsTrue(metadata.ShowForEdit);
			Assert.IsNull(metadata.SimpleDisplayText);
			Assert.IsNull(metadata.TemplateHint);
			Assert.IsNull(metadata.Watermark);
		}

		[Test]
		public void AllModifiedMetadataForProperty()
		{
			_reg.ForType<Customer>().ForProperty(m => m.FirstName, md =>
			                                                       	{
																		md.ConvertEmptyStringToNull(false);
																		md.DataTypeName("PersonName");
			                                                       		md.Description("El nombre del cliente");
			                                                       		md.DisplayFormatString("{0:s}");
			                                                       		md.DisplayName("Nombre");
			                                                       		md.EditFormatString("{1:s}");
			                                                       		md.HideSurroundingHtml(true);
																		md.ReadOnly(true);
			                                                       		md.NullDisplayText("(none)");
			                                                       		md.ShortDisplayName("Nom.");
																		md.ShowForDisplay(false);
																		md.ShowForEdit(false);
			                                                       		md.SimpleDisplayText("Simple text");
			                                                       		md.TemplateHint("CustomerTemplate");
			                                                       		md.Watermark("Watermark");
			                                                       	});

			var metadata = GetPropertyMetadata(typeof(Customer), "FirstName");

			Assert.AreEqual(typeof(Customer), metadata.ContainerType);

			Assert.IsFalse(metadata.ConvertEmptyStringToNull);
			Assert.AreEqual("PersonName", metadata.DataTypeName);
			Assert.AreEqual("El nombre del cliente", metadata.Description);
			Assert.AreEqual("{0:s}", metadata.DisplayFormatString);
			Assert.AreEqual("Nombre", metadata.DisplayName);
			Assert.AreEqual("{1:s}", metadata.EditFormatString);
			Assert.AreEqual("Nombre", metadata.GetDisplayName());
			Assert.IsTrue(metadata.HideSurroundingHtml);
			Assert.IsTrue(metadata.IsReadOnly);
			Assert.AreEqual(typeof(String), metadata.ModelType);
			Assert.AreEqual("(none)", metadata.NullDisplayText);
			Assert.AreEqual(ModelMetadata.DefaultOrder, metadata.Order);
			Assert.AreEqual("FirstName", metadata.PropertyName);
			Assert.AreEqual("Nom.", metadata.ShortDisplayName);
			Assert.IsFalse(metadata.ShowForDisplay);
			Assert.IsFalse(metadata.ShowForEdit);
			Assert.AreEqual("Simple text", metadata.SimpleDisplayText);
			Assert.AreEqual("CustomerTemplate", metadata.TemplateHint);
			Assert.AreEqual("Watermark", metadata.Watermark);
		}

		[Test]
		public void ModifiedMetadataForPropertyFluent()
		{
			_reg.ForType<Customer>().
				ForProperty(m => m.FirstName).DisplayName("Nombre").
				ForProperty(m => m.LastName).DisplayName("Apellido");

			Assert.AreEqual("Nombre", GetPropertyMetadata(typeof(Customer), "FirstName").GetDisplayName());
			Assert.AreEqual("Apellido", GetPropertyMetadata(typeof(Customer), "LastName").GetDisplayName());
		}

		[Test]
		public void ModifiedMetadataForPropertyLambda()
		{
			_reg.ForType<Customer>().
				ForProperty(m => m.FirstName, p => p.DisplayName("Nombre")).
				ForProperty(m => m.LastName, p => p.DisplayName("Apellido"));

			Assert.AreEqual("Nombre", GetPropertyMetadata(typeof(Customer), "FirstName").GetDisplayName());
			Assert.AreEqual("Apellido", GetPropertyMetadata(typeof(Customer), "LastName").GetDisplayName());
		}

		//[Test]
		//public void MetadataForPropertyOnTypeNestedClosure()
		//{
		//    _reg.
		//        ForType<Customer>().
		//            Description("Cliente").
		//            ForProperty(m => m.FirstName, p =>
		//            {
		//                p.StringLength(10);
		//                p.DisplayName("Nombre");
		//            }).
		//            ForProperty(m => m.LastName, p =>
		//            {
		//                p.StringLength(20);
		//                p.DisplayName("Apellido");
		//            }).
		//        ForType<Provider>().
		//            Description("Proveedor");

		//    Assert.AreEqual("Cliente", GetTypeMetadata(typeof(Customer)).Description);
		//    Assert.AreEqual("Nombre", GetPropertyMetadata(typeof(Customer), "FirstName").GetDisplayName());
		//    Assert.AreEqual("Apellido", GetPropertyMetadata(typeof(Customer), "LastName").GetDisplayName());
		//    Assert.AreEqual("Proveedor", GetTypeMetadata(typeof(Provider)).Description);
		//}
	}
}
