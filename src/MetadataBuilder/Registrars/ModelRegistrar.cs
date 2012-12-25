using System;
using System.Linq;
using System.Web.Mvc;
using Tandil.MetadataBuilder.Metadata;
using Tandil.MetadataBuilder.Validation;

namespace Tandil.MetadataBuilder.Registrars
{
	public class ModelRegistrar : IModelRegistrar
	{
		internal ModelRegistrar()
		{
		}

		public ITypeRegistrar<TModel> ForType<TModel>()
		{
			return EnsureTypeIsKnownAndGetTypeRegistrar<TModel>();
		}

		public ITypeRegistrar<TModel> ForType<TModel>(Action<ITypeRegistrar<TModel>> action)
		{
			var typeRegistrar = EnsureTypeIsKnownAndGetTypeRegistrar<TModel>();
			action(typeRegistrar);
			return typeRegistrar;
		}

		public IModelRegistrar WireToRuntime()
		{
			ModelMetadataProviders.Current = new GenericsModelMetadataProvider(ModelMetadataProviders.Current);

			var oldValidatorProvider = ModelValidatorProviders.Providers.Single(p => p is DataAnnotationsModelValidatorProvider);
			ModelValidatorProviders.Providers.Remove(oldValidatorProvider);
			ModelValidatorProviders.Providers.Add(new GenericsValidatorProvider(oldValidatorProvider));

			return this;
		}

		public IModelRegistrar ModelNamespacePattern(string modelNamespacePattern)
		{
			ConfigurationHolder.ModelNamespacePattern = modelNamespacePattern;
			return this;
		}

		public IModelRegistrar DomainAccessorAccessor(Func<Type, IDomainAccessor<object>> function)
		{
			ConfigurationHolder.DomainAccessorAccessor = function;
			return this;
		}

		public IModelRegistrar DomainMutatorAccessor(Func<Type, IDomainMutator<object>> function)
		{
			ConfigurationHolder.DomainMutatorAccessor = function;
			return this;
		}

		public void RegisterPatterApplier(IPatternApplier patternApplier)
		{
			ConfigurationHolder.AddPatternApplier(patternApplier);
		}

		private static ITypeRegistrar<TModel> EnsureTypeIsKnownAndGetTypeRegistrar<TModel>()
		{
			return ConfigurationHolder.EnsureTypeIsKnownAndGetTypeRegistrar<TModel>();
		}
	}
}