using NHibernate;
using NUnit.Framework;

namespace Centros.Tests.Persistence
{
    public abstract class BasePersistenceFixture
    {
        [TestFixtureSetUp]
        public virtual void FixtureSetUp()
        {
            AppInitializer.Initialize();
        }

        [TestFixtureTearDown]
        public virtual void FixtureTearDown()
        {
            AppInitializer.Dispose();
        }

        public TestUnitOfWork NewUoW()
        {
            return new TestUnitOfWork(AppInitializer.Resolve<ISessionFactory>());
        }

        public static T Resolve<T>()
        {
            return AppInitializer.Resolve<T>();
        }

    }
}
