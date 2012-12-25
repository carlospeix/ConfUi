using System.Collections.Generic;

namespace Tandil.MetadataBuilder
{
	public interface IDomainAccessor<out T>
	{
		T Get(object id);
		IEnumerable<T> GetList();
	}
}