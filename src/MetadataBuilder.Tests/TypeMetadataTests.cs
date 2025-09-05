using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tandil.MetadataBuilder;
using Tandil.MetadataBuilder.Metadata;

namespace MetadataBuilder.Tests
{
	[TestClass]
	public class TypeMetadataTests : MetadataTests
	{
		IModelRegistrar _reg;

		[TestInitialize]
		public void SetUp()
		{
			_reg = BuildRegistration();
		}

		[TestMethod]
		public void MetadataForTypeFluent()
		{
			_reg.ForType<Customer>();

			var metadata = GetTypeMetadata(typeof(Customer));
			Assert.IsNull(metadata.Description);
		}

		[TestMethod]
		public void ModifiedMetadataForTypeFluent()
		{
			_reg.ForType<Customer>().Description("Cliente (aprobado)");

			var metadata = GetTypeMetadata(typeof(Customer));
			Assert.AreEqual("Cliente (aprobado)", metadata.Description);
		}

		[TestMethod]
		public void ModifiedMetadataForTypeLambda()
		{
			_reg.ForType<Customer>(m => m.Description("Cliente (aprobado)"));

			var metadata = GetTypeMetadata(typeof(Customer));
			Assert.AreEqual("Cliente (aprobado)", metadata.Description);
		}


		[TestMethod]
		public void TwoModelsMetadataForTypeFluent()
		{
			_reg.
				ForType<Customer>().
				ForType<Provider>();

			Assert.IsNull(GetTypeMetadata(typeof(Customer)).Description);
			Assert.IsNull(GetTypeMetadata(typeof(Provider)).Description);
		}

		[TestMethod]
		public void TwoModelsModifiedMetadataForTypeFluent()
		{
			_reg.
				ForType<Customer>().Description("Cliente (aprobado)").
				ForType<Provider>().Description("Proveedor (aprobado)");

			Assert.AreEqual("Cliente (aprobado)", GetTypeMetadata(typeof(Customer)).Description);
			Assert.AreEqual("Proveedor (aprobado)", GetTypeMetadata(typeof(Provider)).Description);
		}

		[TestMethod]
		public void TwoModelsModifiedMetadataForTypeLambda()
		{
			_reg.
				ForType<Customer>(m => m.Description("Cliente (aprobado)")).
				ForType<Provider>(m => m.Description("Proveedor (aprobado)"));

			Assert.AreEqual("Cliente (aprobado)", GetTypeMetadata(typeof(Customer)).Description);
			Assert.AreEqual("Proveedor (aprobado)", GetTypeMetadata(typeof(Provider)).Description);
		}

		[TestMethod]
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

		[TestMethod]
		public void IdPropertyMetadataForTypeFluent()
		{
			_reg.ForType<Customer>().Id(m => m.Id);

			var metadata = (GenericsModelMetadata)GetTypeMetadata(typeof(Customer));
			var idProp = typeof(Customer).GetMember("Id")[0];
			Assert.AreEqual(idProp, metadata.IdMember);
		}

		[TestMethod]
		public void SearchablePropertyMetadataForTypeFluent()
		{
			_reg.ForType<Customer>().ForProperty(m => m.FirstName).Searchable(true);

			var metadata = (GenericsModelMetadata)GetPropertyMetadata(typeof(Customer), "FirstName");
			Assert.IsTrue(metadata.Searchable);
		}

		[TestMethod]
		public void InitialSortMemberForTypeFluent()
		{
			_reg.ForType<Customer>().InitialSortMember(m => m.FirstName);

			var metadata = (GenericsModelMetadata)GetTypeMetadata(typeof(Customer));
			Assert.AreEqual("FirstName", metadata.InitialSortMember.Name);
		}

		[TestMethod]
		public void InstanceDescriptionForTypeFluent()
		{
			_reg.ForType<Customer>().InstanceDescription(m => m.LastName + ", " + m.FirstName);

			var metadata = (GenericsModelMetadata)GetTypeMetadata(typeof(Customer));
			Assert.AreEqual("Peix, Carlos", metadata.InstanceDescription(new Customer { FirstName = "Carlos", LastName = "Peix" }));
		}

		[TestMethod]
		public void ReadOnlyForTypeFluent()
		{
			_reg.ForType<Customer>();

			var metadata = (GenericsModelMetadata)GetTypeMetadata(typeof(Customer));
			Assert.IsFalse(metadata.IsReadOnly);

			_reg.ForType<Customer>().ReadOnly();

			metadata = (GenericsModelMetadata)GetTypeMetadata(typeof(Customer));
			Assert.IsTrue(metadata.IsReadOnly);
		}

		[TestMethod]
		public void InstanceValidatorForTypeFluent()
		{
			_reg.ForType<Customer>().InstanceValidator(ValidateCustomer);

			var metadata = (GenericsModelMetadata)GetTypeMetadata(typeof(Customer));

			var validationMessages = metadata.Validate(new Customer(), 'A');
			Assert.AreEqual(2, validationMessages.Length);
			Assert.AreEqual("Invalid code", validationMessages[0]);
			Assert.AreEqual("Invalid description", validationMessages[1]);
		}

		private string[] ValidateCustomer(Customer c, object operation)
		{
			return new string[] { "Invalid code", "Invalid description" };
		}

		[TestMethod]
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