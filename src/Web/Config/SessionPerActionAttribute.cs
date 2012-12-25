using System.Web.Mvc;
using NHibernate;
using NHibernate.Context;

namespace Centros.Web.Config
{
    public class SessionPerActionAttribute : IActionFilter, IResultFilter
    {
        private readonly ISessionFactory _sessionFactory;

        public SessionPerActionAttribute(ISessionFactory sessionFactory)
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
                if (filterContext.Controller.ViewData.ModelState.IsValid)
                {
                    session.Flush();
                    session.Transaction.Commit();
                }
                else
                {
                    session.Transaction.Rollback();
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