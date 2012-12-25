using System;
using NHibernate;
using NHibernate.Context;

namespace Centros.Tests.Persistence
{
    public class TestUnitOfWork : IDisposable
    {
        readonly ISession _s;
        readonly ITransaction _t;

        public TestUnitOfWork(ISessionFactory sf)
        {
            _s = sf.OpenSession();
            _t = _s.BeginTransaction();
            CurrentSessionContext.Bind(_s);
        }

        public void Dispose()
        {
            _t.Commit();
            _s.Flush();
        }
    }
}
