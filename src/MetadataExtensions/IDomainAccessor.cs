using System;
using System.Collections.Generic;

namespace MetadataExtensions
{
    public interface IDomainAccessor<T> where T : class
    {
        T Get(Guid id);
        void Add(T entity);
        void Delete(T entity);
        IEnumerable<T> GetList();
    }
}
