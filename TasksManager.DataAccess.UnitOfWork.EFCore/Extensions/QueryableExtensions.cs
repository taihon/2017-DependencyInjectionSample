using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace TasksManager.DataAccess.DbImplementation
{
    public static class QueryableExtensions
    {
        private static readonly MethodInfo _firstOrDefault = GetMethod(nameof(Queryable.FirstOrDefault));
        public static Task<TSource> FirstOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, TSource>(_firstOrDefault, source, cancellationToken);
        }

        private static Task<TResult> ExecuteAsync<TSource, TResult>(
                   MethodInfo operatorMethodInfo,
                   IQueryable<TSource> source,
                   CancellationToken cancellationToken = default)
        {
            if (source.Provider is IAsyncQueryProvider provider)
            {
                if (operatorMethodInfo.IsGenericMethod)
                {
                    operatorMethodInfo = operatorMethodInfo.MakeGenericMethod(typeof(TSource));
                }

                return provider.ExecuteAsync<TResult>(
                    Expression.Call(
                        instance: null,
                        method: operatorMethodInfo,
                        arguments: source.Expression),
                    cancellationToken);
            }

            throw new InvalidOperationException();// CoreStrings.IQueryableProviderNotAsync);
        }

        private static Task<TResult> ExecuteAsync<TSource, TResult>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source,
            LambdaExpression expression,
            CancellationToken cancellationToken = default)
            => ExecuteAsync<TSource, TResult>(
                operatorMethodInfo, source, Expression.Quote(expression), cancellationToken);

        private static Task<TResult> ExecuteAsync<TSource, TResult>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source,
            Expression expression,
            CancellationToken cancellationToken = default)
        {
            if (source.Provider is IAsyncQueryProvider provider)
            {
                operatorMethodInfo
                    = operatorMethodInfo.GetGenericArguments().Length == 2
                        ? operatorMethodInfo.MakeGenericMethod(typeof(TSource), typeof(TResult))
                        : operatorMethodInfo.MakeGenericMethod(typeof(TSource));

                return provider.ExecuteAsync<TResult>(
                    Expression.Call(
                        instance: null,
                        method: operatorMethodInfo,
                        arguments: new[] { source.Expression, expression }),
                    cancellationToken);
            }
            throw new InvalidOperationException();
        }

        private static MethodInfo GetMethod<TResult>(
            string name, int parameterCount = 0, Func<MethodInfo, bool> predicate = null)
            => GetMethod(
                name,
                parameterCount,
                mi => mi.ReturnType == typeof(TResult)
                      && (predicate == null || predicate(mi)));

        private static MethodInfo GetMethod(
            string name, int parameterCount = 0, Func<MethodInfo, bool> predicate = null)
            => typeof(Queryable).GetTypeInfo().GetDeclaredMethods(name)
                .Single(
                    mi => mi.GetParameters().Length == parameterCount + 1
                          && (predicate == null || predicate(mi)));
    }
}
