using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tandil.MetadataBuilder.Metadata;
using Tandil.MetadataBuilder.ModelBinder;
using Tandil.MetadataBuilder.Registrars;
using Tandil.MetadataBuilder.Validation;

namespace Tandil.MetadataBuilder
{
	public class ConfigurationHolder
	{
		private static ModelMetadataMappings _metadataMappings = new ModelMetadataMappings();
		private static ValidationMappings _validationMappings = new ValidationMappings();

		private static IList<IPatternApplier> _patternAppliers = new List<IPatternApplier>();

		public static string ModelNamespacePattern { get; set; }
		public static Func<Type, IDomainAccessor<object>> DomainAccessorAccessor { get; set; }
		public static Func<Type, IDomainMutator<object>> DomainMutatorAccessor { get; set; }

		public static IModelRegistrar GetRootRegistrar()
		{
			return new ModelRegistrar();
		}

		public static ModelMetadataMappings MetadataMappings
		{
			get { return _metadataMappings; }
		}

		public static ValidationMappings ValidationMappings
		{
			get { return _validationMappings; }
		}

		public static void AddBinder(Type modelType, ReferenceModelBinder modelBinder)
		{
			if (!ModelBinders.Binders.ContainsKey(modelType))
				ModelBinders.Binders.Add(modelType, modelBinder);
		}

		public static void AddPatternApplier(IPatternApplier patternApplier)
		{
			_patternAppliers.Add(patternApplier);
		}

		public static void Reset()
		{
			_metadataMappings = new ModelMetadataMappings();
			_validationMappings = new ValidationMappings();
			_patternAppliers = new List<IPatternApplier>();
		}

		internal static ITypeRegistrar<TModel> EnsureTypeIsKnownAndGetTypeRegistrar<TModel>()
		{
			var modelType = typeof(TModel);
			_metadataMappings.RegisterType(modelType);
			_metadataMappings.RegisterType(modelType);
			ApplyPatterns(modelType);
			return new TypeRegistrar<TModel>();
		}

		private static void ApplyPatterns(Type modelType)
		{
			foreach (var patternApplier in _patternAppliers)
				patternApplier.ModelRegistered(modelType);
		}
	}
}