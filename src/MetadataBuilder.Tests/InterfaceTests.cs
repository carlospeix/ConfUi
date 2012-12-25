using System;
using NUnit.Framework;
using Tandil.MetadataBuilder;

namespace MetadataBuilder.Tests
{
	[TestFixture, Ignore("Definicion de interfaces, no tiene codigo ejecutable")]
	public class InterfaceTests
	{
		IModelRegistrar _reg;

		[SetUp]
		public void SetUp()
		{
			_reg = BuildRegistration();
		}

		[Test]
		public void MetadataGlobalConfig()
		{
			_reg.ModelNamespacePattern("MvcGenerics.Tests");
		}

		[Test]
		public void MetadataForTypeFluent()
		{
			_reg.ForType<Customer>().
				Id(p => p.Id).
				Description("Cliente");
		}

		[Test]
		public void MetadataForTypeLambda()
		{
			_reg.
				ForType<Customer>(md => md.Id(m => m.Id).Description("Cliente")).
				ForType<Provider>(md => md.Id(m => m.Code).Description("Proveedor"));
		}

		[Test]
		public void MetadataForTypeNestedClosure()
		{
			_reg.ForType<Customer>(md =>
			                       	{
			                       		md.Id(m => m.Id);
			                       		md.Description("Cliente");
			                       		//md.InstanceDescription(m => m.LastName + ", " + m.FirstName);
			                       	});
		}

		[Test]
		public void MetadataCheckTypes()
		{
			Assert.AreEqual(typeof(Customer), _reg.ForType<Customer>().ModelType);
			Assert.AreEqual(typeof(Customer), _reg.ForType<Customer>().ForProperty(m => m.Id).ModelType);
			Assert.AreEqual(typeof(Int32), _reg.ForType<Customer>().ForProperty(m => m.Id).PropertyType);
		}

		[Test]
		public void MetadataForPropertyOnType()
		{
			_reg.ForType<Customer>().ForProperty(m => m.FirstName).DisplayName("Nombre");
		}

		[Test]
		public void MetadataForPropertyOnTypeNestedClosure()
		{
			_reg.
				ForType<Customer>().
					Id(m => m.Id).
					Description("Cliente").
					ForProperty(m => m.FirstName, p =>
					{
						p.Description("Nombres");
						p.DisplayName("Nombre");
					}).
					ForProperty(m => m.LastName, p =>
					{
						p.Description("Apellidos");
						p.DisplayName("Apellido");
					}).
				ForType<Provider>().
					Id(m => m.Code).
					Description("Producto");
		}

		[Test]
		public void DomainAccessor()
		{
			_reg.DomainAccessorAccessor(GetDomainAccessorAccessor());

			_reg.
				ForType<Customer>().Id(m => m.Id).
					ForProperty(m => m.Country).Reference().
				ForType<Country>().Id(m => m.Id).
					ForProperty(m => m.FirstLaborableDay).EnumDataType(typeof(DayOfWeek));
		}

		private static Func<Type, IDomainAccessor<object>> GetDomainAccessorAccessor()
		{
			return null;
		}

		private static IModelRegistrar BuildRegistration()
		{
			return null;
		}
	}
}