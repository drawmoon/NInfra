using NInfra.LinqExtensions.Models;

namespace NInfra.LinqExtensions.Extensions
{
    public static class SearchQueryableExtensions
    {
        public static IQueryable<TSource> Search<TSource>(this IQueryable<TSource> sources, params SearchExpression[] expressions)
        {
            throw new NotImplementedException();
        }
    }
}
