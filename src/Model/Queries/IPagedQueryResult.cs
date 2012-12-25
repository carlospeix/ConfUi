using System.Collections.Generic;

namespace Centros.Model.Queries
{
    public interface IPagedQueryResult<T>
    {
        int PageSize { get; }
        int PageNumber { get; }
        int TotalCount { get; }
        IEnumerable<T> List { get; }
    }
}