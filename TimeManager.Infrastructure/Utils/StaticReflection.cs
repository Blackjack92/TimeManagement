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

        public static IEnumerable<Type> GetEnumerableOfType<T>()
        {
            List<Type> objects = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("TimeManager")))
            {
                foreach (var type in assembly
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(T))))
                {
                    objects.Add(type);
                }
            }
            return objects;
        }

        public static Type GetTypeOfString(string typeName)
        {
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type type = a.GetTypes().FirstOrDefault(t => t.Name == typeName);
                if (type != null) { return type; }
            }

            return null;
        }

        public static object ParseToObject(Type objectType, object valueToParse)
        {
            MethodInfo mi = objectType.GetMethod("Parse", new Type[] { typeof(string) });
            return mi.Invoke(null, new object[] { valueToParse });
        }

        public static void SetValuesFromObject<T>(T objectToSet, T objectWithValues)
        {
            var propertiesWithValues = objectWithValues.GetType().GetProperties();
            foreach (var prop in propertiesWithValues)
            {
                if (typeof(IList).IsAssignableFrom(prop.PropertyType))
                {
                    
                    var listToAdd = prop.GetValue(objectToSet) as IList;
                    var list = prop.GetValue(objectWithValues) as IList;
                    listToAdd.Clear();

                    foreach (var item in list)
                    {
                        listToAdd.Add(item);
                    }
                }
                else
                {
                    prop.SetValue(objectToSet, prop.GetValue(objectWithValues));
                }
            }
        }
    }
}
