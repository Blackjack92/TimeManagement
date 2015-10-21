using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using TimeManager.Infrastructure.Utils;

namespace TimeManager.Infrastructure.Data
{
    public abstract class DataStoreObject<T>
    {
        public XElement CreateXElement(T element)
        {
            var rootElement = new XElement(typeof(T).Name);

            var properties = new List<Expression<Func<T, object>>>();
            SetProperties(properties);

            foreach (var item in properties)
            {
                var content = item.Compile().Invoke(element);
                Type type = content.GetType();

                if (type.IsGenericType && content is IEnumerable)
                {
                    var test = content as IEnumerable;
                    foreach(var x in test)
                    {
                        Type genericClass = typeof(DataStoreObject<>);
                        //// MakeGenericType is badly named
                        Type constructedClass = genericClass.MakeGenericType(x.GetType());

                        //object created = Activator.CreateInstance(constructedClass);

                        MethodInfo method = typeof(StaticReflection).GetMethod("GetEnumerableOfType").MakeGenericMethod(new Type[] { constructedClass });
                        var super = method.Invoke(null, null);

                    }
                }
                else
                {
                    Type genericClass = typeof(DataStoreObject<>);
                    //// MakeGenericType is badly named
                    Type constructedClass = genericClass.MakeGenericType(type);


                    MethodInfo method = typeof(StaticReflection).GetMethod("GetEnumerableOfType").MakeGenericMethod(new Type[] { constructedClass });
                    //MethodInfo method = typeof(StaticReflection).GetMethod("GetEnumerableOfType").MakeGenericMethod(new Type[] { DataStoreObject<type> });
                    var test = method.Invoke(null, null);
                }

              


                rootElement.Add(new XElement(StaticReflection.GetMemberName(item), content));
            }

            return rootElement;
        }

        protected abstract void SetProperties(List<Expression<Func<T, object>>> selector);
    }


}
