using System;
using System.Collections.Generic;
using NHibernate;
using Centros.Model.Queries;

namespace Centros.Infrastructure.Queries
{
    public class Query<T> : IQuery<T> where T : class
    {
        private readonly ISessionFactory _sessionFactory;

        public Query(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        protected ISession CurrentSession
        {
            get { return _sessionFactory.GetCurrentSession(); }
        }

        //protected abstract void AddRestrictions(IQueryOver<T,int> query);
        //protected abstract void AddOrder(IQueryOver<T,int> query);
        protected virtual void AddOrder(ICriteria c)
        {
        }

        protected virtual void AddRestrictions(ICriteria c)
        {
        }

        public virtual T Get(Guid id)
        {
            return CurrentSession.Get<T>(id);
        }

        public virtual T GetUnique()
        {
            var c = CurrentSession.CreateCriteria<T>();

            AddRestrictions(c);

            return c.UniqueResult<T>();
        }

        public virtual IEnumerable<T> GetList()
        {
            var c = CurrentSession.CreateCriteria<T>();

            AddRestrictions(c);
            AddOrder(c);

            return c.List<T>();
        }

        public virtual IPagedQueryResult<T> GetPagedList(int pageSize, int pageNumber)
        {
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", "Page size debe ser mayor de cero.");

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", "Page number debe ser mayor de cero.");

            var c = CurrentSession.CreateCriteria<T>();

            AddRestrictions(c);
            AddOrder(c);

            //var queryCount = query.ToRowCountQuery();

            c.SetFirstResult(pageNumber - 1).SetMaxResults(pageSize);

            var list = c.List<T>();
            var totalCount = 0; // queryCount.FutureValue<int>().Value;

            return new PagedQueryResult<T>(pageSize, pageNumber, totalCount, list);
        }
    }
}