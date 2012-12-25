using System;
using System.Web.Mvc;
using Castle.Windsor;
using Centros.Model.Repositories;

namespace Centros.Web.Config
{
    public class ReferenceModelBinder : IModelBinder
    {
        readonly IWindsorContainer _container;
        private readonly DefaultModelBinder _defaultModelBinder;

        public ReferenceModelBinder(IWindsorContainer container)
        {
            _container = container;
            _defaultModelBinder = new DefaultModelBinder();
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (IsReferenceBinding(bindingContext))
                return BindReference(bindingContext);

            return _defaultModelBinder.BindModel(controllerContext, bindingContext);
        }

        private static bool IsReferenceBinding(ModelBindingContext bindingContext)
        {
            return bindingContext.ModelMetadata.ContainerType != null;
        }

        private object BindReference(ModelBindingContext bindingContext)
        {
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            
            if (result == null)
                return null;

            var repositoryType = typeof(IRepository<>).MakeGenericType(bindingContext.ModelType);
            var repository = _container.Resolve(repositoryType);

            if (repository == null)
                return null;

            var reference = repositoryType.GetMethod("Get").Invoke(repository,
                                                                   new object[] {BuildGuid(result.AttemptedValue)});

            return reference;
        }

        private static Guid BuildGuid(string formValue)
        {
            Guid retVal;
            if (Guid.TryParse(formValue, out retVal))
                return retVal;
            return Guid.Empty;
        }
    }
}