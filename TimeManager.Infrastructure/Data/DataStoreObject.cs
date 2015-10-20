using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
                rootElement.Add(new XElement(StaticReflection.GetMemberName(item), item.Compile().Invoke(element)));
            }

            return rootElement;
        }

        protected abstract void SetProperties(List<Expression<Func<T, object>>> selector);
    }


}
