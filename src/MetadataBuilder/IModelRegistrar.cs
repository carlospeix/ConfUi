using System;

namespace Tandil.MetadataBuilder
{
	public interface IModelRegistrar
	{
		// Traverse down
		ITypeRegistrar<TModel> ForType<TModel>();
		ITypeRegistrar<TModel> ForType<TModel>(Action<ITypeRegistrar<TModel>> action);

		IModelRegistrar WireToRuntime();
		IModelRegistrar ModelNamespacePattern(string modelNamespacePattern);
		IModelRegistrar DomainAccessorAccessor(Func<Type, IDomainAccessor<object>> function);
		IModelRegistrar DomainMutatorAccessor(Func<Type, IDomainMutator<object>> function);

		void RegisterPatterApplier(IPatternApplier patternApplier);
	}
}