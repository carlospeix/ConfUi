using System;
using System.Collections.Generic;

namespace MetadataExtensions
{
    public interface IDomainReferenceProvider<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetList();
    }
}
