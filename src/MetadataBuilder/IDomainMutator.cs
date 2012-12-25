namespace Tandil.MetadataBuilder
{
	public interface IDomainMutator<in T>
	{
		void Add(T entity);
		void Delete(T entity);
	}
}