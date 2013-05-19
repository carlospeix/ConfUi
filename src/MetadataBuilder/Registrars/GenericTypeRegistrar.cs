using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Tandil.MetadataBuilder.Registrars
{
	public class TypeRegistrar<TModel> : ITypeRegistrar<TModel>
	{
		private readonly TypeRegistrar _registrar;

		internal TypeRegistrar()
		{
			_registrar = new TypeRegistrar(typeof(TModel));
		}

		public ITypeRegistrar<TModelTraverse> ForType<TModelTraverse>()
		{
			return EnsureTypeIsKnownAndGetTypeRegistrar<TModelTraverse>();
		}

		public ITypeRegistrar<TModelTraverse> ForType<TModelTraverse>(Action<ITypeRegistrar<TModelTraverse>> action)
		{
			var typeRegistrar = EnsureTypeIsKnownAndGetTypeRegistrar<TModelTraverse>();
			action(typeRegistrar);
			return typeRegistrar;
		}

		public IPropertyRegistrar<TModel, TPropertyTraverse> ForProperty<TPropertyTraverse>(Expression<Func<TModel, TPropertyTraverse>> expression)
		{
			var memberInfo = TypeExtensions.DecodeMemberAccessExpression(expression);
			return new PropertyRegistrar<TModel, TPropertyTraverse>(memberInfo.Name);
		}

		public IPropertyRegistrar<TModel, TPropertyTraverse> ForProperty<TPropertyTraverse>(Expression<Func<TModel, TPropertyTraverse>> expression,
			Action<IPropertyRegistrar<TModel, TPropertyTraverse>> action)
		{
			var memberInfo = TypeExtensions.DecodeMemberAccessExpression(expression);
			var propertyRegistrar = new PropertyRegistrar<TModel, TPropertyTraverse>(memberInfo.Name);
			action(propertyRegistrar);
			return propertyRegistrar;
		}

		public Type ModelType
		{
			get { return _registrar.ModelType; }
		}

		public ITypeRegistrar<TModel> Id<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			_registrar.Id(TypeExtensions.DecodeMemberAccessExpression(expression));
			return this;
		}

		public ITypeRegistrar<TModel> Id(MemberInfo idMember)
		{
			_registrar.Id(idMember);
			return this;
		}

		public ITypeRegistrar<TModel> Description(string description)
		{
			_registrar.Description(description);
			return this;
		}

		public ITypeRegistrar<TModel> InitialSortMember<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			_registrar.InitialSortMember(TypeExtensions.DecodeMemberAccessExpression(expression));
			return this;
		}

	    public void Validator(Action<TModel> validator)
	    {
	    }

	    //public ITypeRegistrar<TModel> InstanceDescription(Func<TModel, string> function)
		//{
		//    Modifiers.Add(metadata => metadata.InstanceDescription = function);
		//    return this;
		//}

		#region (Comentado) Este codigo estaba en el proyecto MvcConf2011
		//public ITypeRegistrar<TModel> InstanceDescription(Func<TModel, string> function)
		//{
		//    Modifiers.Add(metadata => metadata.SimpleDisplayTextCallback =
		//        _ => GetSimpleDisplayText(metadata, propertyName));
		//    return this;
		//}

		//public ITypeRegistrar<TModel> SimpleDisplayValue(string propertyName)
		//{
		//    Modifiers.Add(metadata => metadata.SimpleDisplayTextCallback = 
		//        _ => GetSimpleDisplayText(metadata, propertyName));
		//    return this;
		//}

		//private static string GetSimpleDisplayText(GenericsModelMetadata metadata, string propertyName)
		//{
		//    var dictionary = (IDictionary)metadata.Model;
		//    var value = dictionary[propertyName];
		//    return value == null ? null : value.ToString();
		//}
		#endregion

		private static ITypeRegistrar<TModelTraverse> EnsureTypeIsKnownAndGetTypeRegistrar<TModelTraverse>()
		{
			return ConfigurationHolder.EnsureTypeIsKnownAndGetTypeRegistrar<TModelTraverse>();
		}
	}
}