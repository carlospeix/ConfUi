using NHibernate;

using Centros.Model;
using Centros.Model.Queries;
using NHibernate.Criterion;

namespace Centros.Infrastructure.Queries
{
    public class QueryCentrosLista : Query<Centro>, IQueryCentrosLista
    {
        public QueryCentrosLista(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        protected override void AddOrder(ICriteria c)
        {
            c.AddOrder(new Order("Nombre", true));
        }
    }

    public class QueryEducadoresLista : Query<Educador>, IQueryEducadoresLista
    {
        public QueryEducadoresLista(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        protected override void AddOrder(ICriteria c)
        {
            c.AddOrder(new Order("Apellido", true)).AddOrder(new Order("Nombre", true));
        }
    }

    public class QueryInstitucionesLista : Query<Institucion>, IQueryInstitucionesLista
    {
        public QueryInstitucionesLista(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        protected override void AddOrder(ICriteria c)
        {
            c.AddOrder(new Order("Nombre", true));
        }
    }

    public class QueryOrganizacionesLista : Query<Organizacion>, IQueryOrganizacionesLista
    {
        public QueryOrganizacionesLista(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        protected override void AddOrder(ICriteria c)
        {
            c.AddOrder(new Order("Nombre", true));
        }
    }

    public class QueryJurisdiccionesLista : Query<Jurisdiccion>, IQueryJurisdiccionesLista
    {
        public QueryJurisdiccionesLista(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        protected override void AddOrder(ICriteria c)
        {
            c.AddOrder(new Order("Nombre", true));
        }
    }
}