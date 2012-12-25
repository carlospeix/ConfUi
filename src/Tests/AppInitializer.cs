using System.Reflection;

using NHibernate;
using Castle.Windsor;
using Castle.MicroKernel.Registration;

using Centros.Infrastructure;
using Centros.Infrastructure.Queries;
using Centros.Infrastructure.Repositories;
using Centros.Tests.Persistence;
using Centros.Model.Queries;
using Centros.Model.Repositories;

namespace Centros.Tests
{
    public static class AppInitializer
    {
        private static IWindsorContainer _container;

        public static void Initialize()
        {
            CreateContainer();
            InitializeNHibernate();
            RegisterRepositories();
            RegisterQueries();
            RegisterDataInitializer();
        }

        public static void InitializeData()
        {
            var dataInitializer = _container.Resolve<DataInitializer>();
            dataInitializer.Initialize();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public static void Dispose()
        {
            _container.Dispose();
        }

        private static void CreateContainer()
        {
            _container = new WindsorContainer();

            // register the container with itself to be able to resolve 
            // the dependency in the ctor of WindsorControllerFactory
            _container.Register(Component.For<IWindsorContainer>().
                Instance(_container).LifeStyle.Singleton);
        }

        private static void InitializeNHibernate()
        {
            var inititializer = new NhInitializer();
            inititializer.InitializeForTests();
            inititializer.DropSchema();
            inititializer.CreateSchema();

            _container.Register(
                Component.For<ISessionFactory>()
                    .Instance(inititializer.SessionFactory)
                    .LifeStyle.Singleton);
        }

        private static void RegisterRepositories()
        {
            _container.Register(
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(Repository<>))
                    .LifeStyle.Transient);
        }

        private static void RegisterQueries()
        {
            _container.Register(AllTypes
                .FromAssembly(Assembly.GetAssembly(typeof(Query<>)))
                .BasedOn(typeof(IQuery<>))
                .WithService.AllInterfaces());
        }

        private static void RegisterDataInitializer()
        {
            _container.Register(Component.For<DataInitializer>());
        }
    }
}