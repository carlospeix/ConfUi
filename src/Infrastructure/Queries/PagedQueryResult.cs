using System.Collections.Generic;
using Centros.Model.Queries;

namespace Centros.Infrastructure.Queries
{
    public class PagedQueryResult<T> : IPagedQueryResult<T> where T : class
    {
        public PagedQueryResult(int pageSize, int pageNumber, int totalCount, IEnumerable<T> list)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalCount = totalCount;
            List = list;
        }

        public int PageSize { get; private set; }
        public int PageNumber { get; private set; }
        public int TotalCount { get; private set; }
        public IEnumerable<T> List  { get; private set; }
    }
}