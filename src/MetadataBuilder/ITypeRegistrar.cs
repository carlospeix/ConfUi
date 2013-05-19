using System;
using System.Linq.Expressions;

namespace Tandil.MetadataBuilder
{
	public interface ITypeRegistrar<TModel>
	{
		// Traverse sideways
		ITypeRegistrar<TModelTraverse> ForType<TModelTraverse>();
		ITypeRegistrar<TModelTraverse> ForType<TModelTraverse>(Action<ITypeRegistrar<TModelTraverse>> action);

		// Traverse down
		IPropertyRegistrar<TModel, TPropertyTraverse> ForProperty<TPropertyTraverse>(
			Expression<Func<TModel, TPropertyTraverse>> expression);
		IPropertyRegistrar<TModel, TPropertyTraverse> ForProperty<TPropertyTraverse>(
			Expression<Func<TModel, TPropertyTraverse>> expression,
			Action<IPropertyRegistrar<TModel, TPropertyTraverse>> action);

		Type ModelType { get; }

		// Configuration
		ITypeRegistrar<TModel> Id<TProperty>(Expression<Func<TModel, TProperty>> expression);
		ITypeRegistrar<TModel> Description(string description);
		ITypeRegistrar<TModel> InitialSortMember<TProperty>(Expression<Func<TModel, TProperty>> expression);
		//ITypeRegistrar<TModel> InstanceDescription(Func<TModel, string> function);
		//ITypeRegistrar<TModel> ListOrder<TProperty>(Expression<Func<TModel, TProperty>> expression, string order);

        void Validator(Action<TModel> validator);
	}
}