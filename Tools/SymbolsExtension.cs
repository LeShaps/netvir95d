using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Netvir.Utilities
{
    class SymbolsExtensions
    {
        public static MethodInfo GetMethodInfo(Expression<Action> Expression)
            => GetMethodInfo((LambdaExpression)Expression);

        public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> Expression)
            => GetMethodInfo((LambdaExpression)Expression);

        public static MethodInfo GetMethodInfo(LambdaExpression Expression)
        {
            MethodCallExpression OuterMostExpression = Expression.Body as MethodCallExpression;

            if (OuterMostExpression == null) {
                throw new ArgumentException("Invalid expression. Expression should consist of a Method call only");
            }

            return OuterMostExpression.Method;
        }
    }
}
