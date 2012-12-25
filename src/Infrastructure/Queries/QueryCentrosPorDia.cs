using System;
using NHibernate;
using System.Collections.Generic;

using Centros.Model;
using Centros.Model.Queries;

namespace Centros.Infrastructure.Queries
{
    public class QueryCentrosPorDia : Query<Centro>, IQueryCentrosPorDia
    {
        public QueryCentrosPorDia(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        private DayOfWeek? _dia;
        public IQueryCentrosPorDia ConDia(DayOfWeek dayOfWeek)
        {
            _dia = dayOfWeek;
            return this;
        }

        public override IEnumerable<Centro> GetList()
        {
            if (!_dia.HasValue)
                throw new InvalidOperationException("Debe especificar un valor para 'dia' al usar este query.");

            var q = CurrentSession.CreateQuery("from Centro c where c.Horario.Dia = :dia");
            q.SetParameter("dia", _dia.Value);

            return q.List<Centro>();
        }
    }
}