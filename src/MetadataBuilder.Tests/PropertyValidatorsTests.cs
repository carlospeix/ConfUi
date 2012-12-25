using NUnit.Framework;
using Tandil.MetadataBuilder;

namespace MetadataBuilder.Tests
{
	[TestFixture]
	public class PropertyValidatorsTests : MetadataTests
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
			//_reg.ForType<Customer>().ForProperty(m => m.FirstName, md =>
			//                                                        {
			//                                                            md.Required("Debe ingresar un valor");
			//                                                            md.Range(1, 10, "El valor debe estar entre 0 y 10");
			//                                                            md.RegularExpression("pattern", "El dato no coincide");
			//                                                            md.StringLength(200, 3, "El string debe medir entre 3 y 200");
			//                                                        });

			//var validators = GetPropertyValidators(typeof(Customer), "FirstName");
			//Assert.AreEqual(4, validators.Count());
		}
	}
}
