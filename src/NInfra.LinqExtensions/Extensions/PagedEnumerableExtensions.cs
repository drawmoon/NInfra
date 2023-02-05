using System;

namespace NInfra.LinqExtensions.Extensions
{
	public static class PagedEnumerableExtensions
	{
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TEntity> PageBy<TEntity>(this IEnumerable<TEntity> query, int? page, int pageSize = 10)
        {
            const int defaultPageNumber = 1;

            // Check if the page number is greater then zero - otherwise use default page number
            if (page is null or <= 0)
            {
                page = defaultPageNumber;
            }

            return query.Skip((page.Value - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedList<TEntity> ToPagedListAsync<TEntity>(this IEnumerable<TEntity> query, int? page, int pageSize = 10)
        {
            const int defaultPageNumber = 1;

            // Check if the page number is greater then zero - otherwise use default page number
            if (page is null or <= 0)
            {
                page = defaultPageNumber;
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = query.PageBy(page, pageSize).ToList();

            return new PagedList<TEntity>
            {
                PageIndex = page.Value,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Items = items,
                HasNextPages = page < totalPages,
                HasPrevPages = page - 1 > 0
            };
        }
    }
}

