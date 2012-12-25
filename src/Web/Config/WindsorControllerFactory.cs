using System;
using System.Web.Mvc;
using Castle.Windsor;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using System.Reflection;
using Centros.Web.Controllers;
using Tandil.MetadataBuilder;

namespace Centros.Web.Config
{
    // http://blog.andreloker.de/post/2009/03/28/ASPNET-MVC-with-Windsor-programmatic-controller-registration.aspx
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        readonly IWindsorContainer _container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            _container = container;

            RegisterControllers();
        }

		protected override Type GetControllerType(RequestContext requestContext, string controllerName)
		{
			if (controllerName.Equals("Generics"))
				return BuildClosedGenericControllerType(requestContext.RouteData.Values["type"]);

			return base.GetControllerType(requestContext, controllerName);
		}

		private static Type BuildClosedGenericControllerType(object typeParam)
		{
			var typeFullName = String.Format(ConfigurationHolder.ModelNamespacePattern, typeParam);
			var targetType = Type.GetType(typeFullName, false);
			if (targetType == null)
				throw new InvalidOperationException(String.Format("El tipo {0} es desconocido.", typeFullName));
			return typeof(GenericsController<>).MakeGenericType(targetType);
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return base.GetControllerInstance(requestContext, controllerType);

            return _container.Resolve(controllerType) as IController;
        }

        public override void ReleaseController(IController controller)
        {
            _container.Release(controller);
        }

        private void RegisterControllers()
        {
            _container.Register(
                AllTypes.FromAssembly(Assembly.GetExecutingAssembly()).
                BasedOn<IController>().
                Configure(c => c.LifeStyle.Transient));
        }
    }
}