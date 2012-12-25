using System;
using System.Linq;
using NUnit.Framework;

using Centros.Model.Queries;

namespace Centros.Tests.Persistence
{
    public class QueriesTests : BasePersistenceFixture
    {
        public override void FixtureSetUp()
        {
            base.FixtureSetUp();
            AppInitializer.InitializeData();
        }

        [Test]
        [TestCase(DayOfWeek.Sunday)]
        [TestCase(DayOfWeek.Tuesday)]
        [TestCase(DayOfWeek.Thursday)]
        [TestCase(DayOfWeek.Friday)]
        [TestCase(DayOfWeek.Saturday)]
        public void NoDeberiaHaberCentrosSalvoLunesYMiercoles(DayOfWeek dia)
        {
            using (NewUoW())
            {
                var query = Resolve<IQueryCentrosPorDia>();
                query.ConDia(dia);
                Assert.AreEqual(0, query.GetList().Count());
            }
        }

        [Test]
        public void DeberiaHaberUnCentroElLunes()
        {
            using (NewUoW())
            {
                var query = Resolve<IQueryCentrosPorDia>();
                query.ConDia(DayOfWeek.Monday);
                Assert.AreEqual(1, query.GetList().Count());
            }
        }

        [Test]
        public void DeberiaHaberDosCentroElMiercoles()
        {
            using (NewUoW())
            {
                var query = Resolve<IQueryCentrosPorDia>();
                query.ConDia(DayOfWeek.Wednesday);
                Assert.AreEqual(2, query.GetList().Count());
            }
        }
    }
}
