using System;
using System.Reflection;

namespace Tandil.MetadataBuilder.PatternAppliers
{
	/// <summary>
	/// Configura la propiedad Id y la oculta en la UI
	/// </summary>
	public class IdPatternApplier : BasePatternApplier
	{
		public string IdPropertyName { get; set; }

		public override void ModelRegistered(Type modelType)
		{
			var typeRegistrar = BuildTypeRegistrar(modelType);
			var idProperty = modelType.GetProperty(IdPropertyName);
			if (idProperty != null)
			{
				typeRegistrar.Id(idProperty);
				ConfigureProperty(idProperty);
			}
		}

		private void ConfigureProperty(PropertyInfo idProperty)
		{
			var propertyRegistrar = BuildPropertyRegistrar(idProperty);
			propertyRegistrar.HiddenInput(false);
		}
	}
}