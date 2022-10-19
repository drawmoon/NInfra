using NInfra.LinqExtensions.Models;

namespace NInfra.LinqExtensions.Extensions
{
    public static class SearchEnumerableExtensions
    {
        public static IEnumerable<TSource> Search<TSource>(this IEnumerable<TSource> sources, params SearchExpression[] expressions)
        {
            throw new NotImplementedException();
        }
    }
}
