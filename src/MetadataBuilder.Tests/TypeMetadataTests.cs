using System;
using NUnit.Framework;
using Tandil.MetadataBuilder;
using Tandil.MetadataBuilder.Metadata;

namespace MetadataBuilder.Tests
{
	[TestFixture]
	public class TypeMetadataTests : MetadataTests
	{
		IModelRegistrar _reg;

		[SetUp]
		public void SetUp()
		{
			_reg = BuildRegistration();
		}

		[Test]
		public void MetadataForTypeFluent()
		{
			_reg.ForType<Customer>();

			var metadata = GetTypeMetadata(typeof(Customer));
			Assert.IsNull(metadata.Description);
		}

		[Test]
		public void ModifiedMetadataForTypeFluent()
		{
			_reg.ForType<Customer>().Description("Cliente (aprobado)");

			var metadata = GetTypeMetadata(typeof(Customer));
			Assert.AreEqual("Cliente (aprobado)", metadata.Description);
		}

		[Test]
		public void ModifiedMetadataForTypeLambda()
		{
			_reg.ForType<Customer>(m => m.Description("Cliente (aprobado)"));

			var metadata = GetTypeMetadata(typeof(Customer));
			Assert.AreEqual("Cliente (aprobado)", metadata.Description);
		}


		[Test]
		public void TwoModelsMetadataForTypeFluent()
		{
			_reg.
				ForType<Customer>().
				ForType<Provider>();

			Assert.IsNull(GetTypeMetadata(typeof(Customer)).Description);
			Assert.IsNull(GetTypeMetadata(typeof(Provider)).Description);
		}

		[Test]
		public void TwoModelsModifiedMetadataForTypeFluent()
		{
			_reg.
				ForType<Customer>().Description("Cliente (aprobado)").
				ForType<Provider>().Description("Proveedor (aprobado)");

			Assert.AreEqual("Cliente (aprobado)", GetTypeMetadata(typeof(Customer)).Description);
			Assert.AreEqual("Proveedor (aprobado)", GetTypeMetadata(typeof(Provider)).Description);
		}

		[Test]
		public void TwoModelsModifiedMetadataForTypeLambda()
		{
			_reg.
				ForType<Customer>(m => m.Description("Cliente (aprobado)")).
				ForType<Provider>(m => m.Description("Proveedor (aprobado)"));

			Assert.AreEqual("Cliente (aprobado)", GetTypeMetadata(typeof(Customer)).Description);
			Assert.AreEqual("Proveedor (aprobado)", GetTypeMetadata(typeof(Provider)).Description);
		}

		[Test]
		public void TwoModelsModifiedMetadataForTypeNestedClosure()
		{
			_reg.
				ForType<Customer>(md =>
				{
					md.Id(model => model.Id);
					md.Description("Cliente (aprobado)");
				}).
				ForType<Provider>(md =>
				{
					md.Id(model => model.Code);
					md.Description("Proveedor (aprobado)");
				});

			Assert.AreEqual("Cliente (aprobado)", GetTypeMetadata(typeof(Customer)).Description);
			Assert.AreEqual("Proveedor (aprobado)", GetTypeMetadata(typeof(Provider)).Description);
		}

		[Test]
		public void IdPropertyMetadataForTypeFluent()
		{
			_reg.ForType<Customer>().Id(m => m.Id);

			var metadata = (GenericsModelMetadata)GetTypeMetadata(typeof(Customer));
			var idProp = typeof(Customer).GetMember("Id")[0];
			Assert.AreEqual(idProp, metadata.IdMember);
		}

		[Test]
		public void SearchablePropertyMetadataForTypeFluent()
		{
			_reg.ForType<Customer>().ForProperty(m => m.FirstName).Searchable(true);

			var metadata = (GenericsModelMetadata)GetPropertyMetadata(typeof(Customer), "FirstName");
			Assert.IsTrue(metadata.Searchable);
		}

		[Test]
		public void InitialSortMemberForTypeFluent()
		{
			_reg.ForType<Customer>().InitialSortMember(m => m.FirstName);

			var metadata = (GenericsModelMetadata)GetTypeMetadata(typeof(Customer));
			Assert.AreEqual("FirstName", metadata.InitialSortMember.Name);
		}

        [Test]
        public void InstanceDescriptionForTypeFluent()
        {
            _reg.ForType<Customer>().InstanceDescription(m => m.LastName + ", " + m.FirstName);

            var metadata = (GenericsModelMetadata)GetTypeMetadata(typeof(Customer));
            Assert.AreEqual("Peix, Carlos", metadata.InstanceDescription(new Customer { FirstName = "Carlos", LastName = "Peix"}));
        }

        [Test]
        public void InstanceValidationForTypeFluent()
        {
            var error = false;
            _reg.ForType<Customer>().InstanceValidator(m => error = String.IsNullOrEmpty(m.LastName));

            var metadata = (GenericsModelMetadata)GetTypeMetadata(typeof(Customer));
            metadata.InstanceValidator(new Customer());
            Assert.IsTrue(error);
        }

        [Test]
		public void DomainAccessorMetadataForTypeFluent()
		{
			//_reg.DomainProviderAccessor(GetTestDomainProvider());

			//var metadata = (GenericsModelMetadata)GetTypeMetadata(typeof(Customer));
			//IDomainProvider<Customer> provider = metadata.DomainProviderAccessor().Invoke();
			//Assert.IsNotNull(provider);
			//Assert.IsInstanceOf(typeof(TestDomainProvider<Customer>), provider);
		}

		//private Func<T, IDomainProvider<object>> GetTestDomainProvider<T>()
		//{
		//    return (modelType) => new TestDomainProvider<T>();
		//}
	}
}