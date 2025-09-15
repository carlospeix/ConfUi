using Microsoft.AspNetCore.Mvc.Filters;
using NHibernate;
using NHibernate.Context;

namespace WebGeneric.Config
{
    public class SessionPerActionFilter : IActionFilter, IResultFilter
    {
        private readonly ISessionFactory _sessionFactory;

        public SessionPerActionFilter(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = _sessionFactory.OpenSession();
            session.FlushMode = FlushMode.Auto;
            CurrentSessionContext.Bind(session);
            session.BeginTransaction();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var session = _sessionFactory.GetCurrentSession();

            try
            {
                if (filterContext.ModelState.IsValid)
                {
                    session.Flush();
                    session.GetCurrentTransaction().Commit();
                }
                else
                {
                    session.GetCurrentTransaction().Rollback();
                }
            }
            finally
            {
                session.Dispose();
            }

            CurrentSessionContext.Unbind(_sessionFactory);
        }
    }
}