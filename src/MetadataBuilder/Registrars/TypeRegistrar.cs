using System;
using System.Collections.Generic;
using System.Reflection;
using Tandil.MetadataBuilder.Metadata;

namespace Tandil.MetadataBuilder.Registrars
{
	public class TypeRegistrar
	{
		private readonly Type _modelType;

		internal TypeRegistrar(Type modelType)
		{
			_modelType = modelType;
		}

		public Type ModelType
		{
			get { return _modelType; }
		}

		public void Id(MemberInfo idMember)
		{
			Modifiers.Add(metadata => metadata.IdMember = idMember);
		}

		public void Description(string description)
		{
			Modifiers.Add(metadata => metadata.Description = description);
		}

		public void InitialSortMember(MemberInfo initialSortMember)
		{
			Modifiers.Add(metadata => metadata.InitialSortMember = initialSortMember);
		}

        public void InstanceValidator(Action<object> validator)
        {
            Modifiers.Add(metadata => metadata.InstanceValidator = validator);
        }

        public void InstanceDescription(Func<object, string> function)
        {
            Modifiers.Add(metadata => metadata.InstanceDescription = function);
        }

        private ICollection<Action<GenericsModelMetadata>> Modifiers
		{
			get { return ConfigurationHolder.MetadataMappings[ModelType].Modifiers; }
		}
    }
}