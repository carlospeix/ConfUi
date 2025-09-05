using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tandil.MetadataBuilder;

namespace MetadataBuilder.Tests
{
	[TestClass]
	public class ModelMetadataTests : MetadataTests
	{
		IModelRegistrar _reg;

		[TestInitialize]
		public void SetUp()
		{
			_reg = BuildRegistration();
		}

		[TestMethod]
		public void ModelNamespacePattern()
		{
			_reg.ModelNamespacePattern("MvcGenerics.Tests.{0}, MvcGenerics.Tests");
			Assert.AreEqual("MvcGenerics.Tests.{0}, MvcGenerics.Tests", ConfigurationHolder.ModelNamespacePattern);
		}
	}
}