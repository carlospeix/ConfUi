using Centros.Model.Repositories;
using Tandil.MetadataBuilder;

namespace WebGeneric.Config
{
    public class DomainMutator<T> : IDomainMutator<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public DomainMutator(IRepository<T> repository)
        {
            _repository = repository;
        }

        public void Add(T entity)
        {
            _repository.Add(entity);
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }
    }
}