using System;

namespace Centros.Model.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(Guid id);
        void Add(T entity);
        void Delete(T entity);
    }
}
