using NInfra.LinqExtensions.Internal;
using NInfra.LinqExtensions.Models;
using System.Linq.Expressions;

namespace NInfra.LinqExtensions.Extensions
{
    public static class SortByQueryableExtensions
    {
        public static IQueryable<TSource> SortBy<TSource>(this IQueryable<TSource> sources,
            params SortByExpression[] expressions) => sources.SortBy(action: null, expressions);

        public static IQueryable<TSource> SortBy<TSource>(this IQueryable<TSource> sources,
            Dictionary<string, string> subs, params SortByExpression[] expressions)
        {
            void Prepare(SortByExpression expression)
            {
                if (subs.TryGetValue(expression.Left, out var val))
                    expression.Left = val;
            }

            return sources.SortBy(Prepare, expressions);
        }

        private static IQueryable<TSource> SortBy<TSource>(this IQueryable<TSource> sources, Action<SortByExpression>? action, params SortByExpression[] expressions)
        {
            var sourceType = typeof(TSource);
            var parameterExpression = Expression.Parameter(sourceType);

            var orderedQueryable = sources;
            foreach (var expression in expressions)
            {
                var op = expression.Op;
                if (op == SortOperator.None)
                    continue;

                action?.Invoke(expression);

                var keySelector = ExpressionUtils.BuildKeySelector<TSource>(parameterExpression, expression.Left);
                orderedQueryable = op == SortOperator.Asc
                    ? sources.OrderBy(keySelector)
                    : sources.OrderByDescending(keySelector);
            }

            return orderedQueryable;
        }
    }
}
