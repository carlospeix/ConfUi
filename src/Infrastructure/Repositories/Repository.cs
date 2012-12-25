using System;
using NHibernate;

using Centros.Model.Repositories;

namespace Centros.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ISessionFactory _sessionFactory;

        public Repository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        protected ISession CurrentSession
        {
            get { return _sessionFactory.GetCurrentSession(); }
        }

        public T Get(Guid id)
        {
            return CurrentSession.Get<T>(id);
        }

        public void Add(T entity)
        {
            CurrentSession.SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            CurrentSession.Delete(entity);
        }
    }
}