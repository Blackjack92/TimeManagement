using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TimeManager.Infrastructure.Utils
{
    public static class StaticReflection
    {
        public static string GetMemberName<T>(this T instance, Expression<Func<T, object>> expression)
        {
            return GetMemberName(expression);
        }

        public static string GetMemberName<T>(Expression<Func<T, object>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(this T instance, Expression<Action<T>> expression)
        {
            return GetMemberName(expression);
        }

        public static string GetMemberName<T>(Expression<Action<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }

        private static string GetMemberName(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("The expression cannot be null.");
            }

            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression = (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression = (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }

            throw new ArgumentException("Invalid expression");
        }

        private static string GetMemberName(UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression = (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ((MemberExpression)unaryExpression.Operand).Member.Name;
        }

        //public static IEnumerable<T> GetEnumerableOfType<T>(params object[] contstructorArguments)
        public static IEnumerable<Type> GetEnumerableOfType<T>()
        {
            //List<T> objects = new List<T>();
            List<Type> objects = new List<Type>();
            foreach (var type in Assembly.GetAssembly(typeof(T))
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(T))))
            {
                //objects.Add((T)Activator.CreateInstance(type, contstructorArguments));
                objects.Add(type);
            }

            //objects.Sort();

            return objects;
        } 
    }
}
