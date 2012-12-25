using System;
using System.Collections.Generic;
using Centros.Model.Queries;
using Tandil.MetadataBuilder;

namespace WebGeneric.Config
{
    public class DomainAccessor<T> : IDomainAccessor<T> where T : class
    {
        private readonly IQuery<T> _query;

		public DomainAccessor(IQuery<T> query)
        {
            _query = query;
        }

        public T Get(object id)
        {
            return _query.Get((Guid)id);
        }

        public IEnumerable<T> GetList()
        {
            return _query.GetList();
        }
    }
}