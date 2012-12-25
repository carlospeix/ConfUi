using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ConfOrm;
using ConfOrm.NH;
using ConfOrm.Shop.Appliers;
using ConfOrm.Shop.CoolNaming;
using ConfOrm.Shop.Inflectors;
using ConfOrm.Shop.Packs;
using ConfOrm.Shop.InflectorNaming;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

using Centros.Model;
using NHibernate.Context;

namespace Centros.Infrastructure
{
    public class NhInitializer
    {
		private readonly string _connectionString;

        private Configuration _configure;
        private ISessionFactory _sessionFactory;

        private readonly Type[] _baseTypesToRecognizeRootEntities = new[] { typeof(Entity) };
		private readonly Type[] _tablePerClassHierarchy = new Type[] {};
		private readonly Type[] _tablePerConcreteClass = new Type[] {};

        public NhInitializer()
        {
            _connectionString = 
                System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }

		#region NH Startup
		public ISessionFactory SessionFactory
		{
			get { return _sessionFactory ?? (_sessionFactory = _configure.BuildSessionFactory()); }
		}

        public void InitializeForWeb()
        {
            Initialize();
            _configure.CurrentSessionContext<WebSessionContext>();
        }

        public void InitializeForTests()
        {
            Initialize();
            _configure.CurrentSessionContext<CallSessionContext>();
        }

        private void Initialize()
		{
            _configure = new Configuration();
            _configure.SessionFactoryName("Centros");
            _configure.Proxy(p => p.ProxyFactoryFactory<ProxyFactoryFactory>());
            _configure.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2008Dialect>();
                db.Driver<SqlClientDriver>();
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.IsolationLevel = IsolationLevel.ReadCommitted;
                db.ConnectionString = _connectionString;
                db.BatchSize = 20;
                db.Timeout = 10;
                db.HqlToSqlSubstitutions = "true 1, false 0, yes 'Y', no 'N'";
            });
            Map();
        }

        private void Map()
        {
            _configure.AddDeserializedMapping(GetMapping(), "MysticDomain");
        }

		public void CreateSchema()
		{
			new SchemaExport(_configure).Create(false, true);
		}

        public void UpdateSchema()
        {
            new SchemaUpdate(_configure).Execute(false, true);
        }

        public void DropSchema()
		{
			new SchemaExport(_configure).Drop(false, true);
		}
		#endregion

		#region Utils
		private bool IsRootEntity(Type type)
		{
			Type baseType = type.BaseType;
			return baseType != null && !IsOnlyBaseTypesToRecognizeRootEntities(type) &&
			       (_baseTypesToRecognizeRootEntities.Contains(baseType) || (baseType.IsGenericType && _baseTypesToRecognizeRootEntities.Contains(baseType.GetGenericTypeDefinition())));
		}

		private bool IsOnlyBaseTypesToRecognizeRootEntities(Type type)
		{
			return (_baseTypesToRecognizeRootEntities.Contains(type) || (type.IsGenericType && _baseTypesToRecognizeRootEntities.Contains(type.GetGenericTypeDefinition())));
		}
		#endregion

		public HbmMapping GetMapping()
		{
			return GetMapper().CompileMappingFor(GetDomainEntities());
		}

		public IEnumerable<HbmMapping> GetMappings()
		{
			return GetMapper().CompileMappingForEach(GetDomainEntities());
		}

		private static IEnumerable<Type> GetDomainEntities()
		{
			List<Type> domainEntities = typeof(Entity).Assembly.GetTypes()
				.Where(t => (typeof(AbstractEntity<int>).IsAssignableFrom(t) || typeof(AbstractEntity<Guid>).IsAssignableFrom(t))
				            && !t.IsGenericType)
				.ToList();
			return domainEntities;
		}

		private Mapper GetMapper()
		{
			#region Initialize ConfORM
			var inflector = new SpanishInflector();

			var orm = new ObjectRelationalMapper();

			IPatternsAppliersHolder patternsAppliers =
				(new SafePropertyAccessorPack())
					.Merge(new SafePoidPack())
					.Merge(new OneToOneRelationPack(orm))
					.Merge(new BidirectionalManyToManyRelationPack(orm))
					.Merge(new BidirectionalOneToManyRelationPack(orm))
					.Merge(new DiscriminatorValueAsClassNamePack(orm))
					.Merge(new PluralizedTablesPack(orm, inflector))
					.Merge(new CoolColumnsNamingPack(orm))
					.UnionWith(new ConfOrm.Shop.InflectorNaming.CollectionOfElementsColumnApplier(orm, inflector))
					.Merge(new PolymorphismPack(orm))
					.Merge(new TablePerClassPack())
					.Merge(new UseNoLazyForNoProxablePack()) // <== Lazy false when the class is not proxable
					.Merge(new UnidirectionalOneToManyMultipleCollectionsKeyColumnApplier(orm))
					.Merge(new UseCurrencyForDecimalApplier());

			// orm.Patterns.PoidStrategies.Add(new HighLowPoidPattern(new {max_lo = 100}));

			var mapper = new Mapper(orm, patternsAppliers);

			IEnumerable<Type> tablePerClassEntities = 
                typeof(Entity).Assembly.GetTypes().Where(
                    t => IsRootEntity(t) && !_tablePerClassHierarchy.Contains(t) && !_tablePerConcreteClass.Contains(t)).ToList();

			// Mappings
			orm.TablePerClass(tablePerClassEntities);
			orm.TablePerClassHierarchy(_tablePerClassHierarchy);
			orm.TablePerConcreteClass(_tablePerConcreteClass);
			#endregion

			ConfOrmMapping(orm, mapper);

			return mapper;
		}

		private static void ConfOrmMapping(ObjectRelationalMapper orm, Mapper mapper)
		{
		}
    }
}