using System;
using Castle.Windsor;
using Tandil.MetadataBuilder;
using WebGeneric.Controllers;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebGeneric.Config
{
	// http://blog.andreloker.de/post/2009/03/28/ASPNET-MVC-with-Windsor-programmatic-controller-registration.aspx
	public class WindsorControllerFactory : IControllerFactory
    {
        readonly IWindsorContainer _container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            _container = container;

            RegisterControllers();
        }

		private static Type BuildClosedGenericControllerType(object typeParam)
		{
			var typeFullName = String.Format(ConfigurationHolder.ModelNamespacePattern, typeParam);
			var targetType = Type.GetType(typeFullName, false);
			if (targetType == null)
				throw new InvalidOperationException(String.Format("El tipo {0} es desconocido.", typeFullName));
			return typeof(GenericController<>).MakeGenericType(targetType);
		}

		private void RegisterControllers()
		{
			//_container.Register(
			//	AllTypes.FromAssembly(Assembly.GetExecutingAssembly()).
			//	BasedOn<System.Web.Mvc.IController>().
			//	Configure(c => c.LifeStyle.Transient));
		}

		public object CreateController(ControllerContext context)
		{
			if (context.ActionDescriptor.ControllerName.Equals("Generic"))
				return BuildClosedGenericControllerType(context.ActionDescriptor.ControllerTypeInfo.AsType());

			return Activator.CreateInstance(context.ActionDescriptor.ControllerTypeInfo.AsType());
		}

		public void ReleaseController(ControllerContext context, object controller)
		{
			_container.Release(controller);
		}
	}
}