using NUnit.Framework;
using Tandil.MetadataBuilder;

namespace MetadataBuilder.Tests
{
	[TestFixture]
	public class ModelMetadataTests : MetadataTests
	{
		IModelRegistrar _reg;

		[SetUp]
		public void SetUp()
		{
			_reg = BuildRegistration();
		}

		[Test]
		public void ModelNamespacePattern()
		{
			_reg.ModelNamespacePattern("MvcGenerics.Tests.{0}, MvcGenerics.Tests");
			Assert.AreEqual("MvcGenerics.Tests.{0}, MvcGenerics.Tests", ConfigurationHolder.ModelNamespacePattern);
		}
	}
}