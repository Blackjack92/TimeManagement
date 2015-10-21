using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using TimeManager.Infrastructure.Utils;

namespace TimeManager.Infrastructure.Data
{
    // TODO: Add a static caching of the used DataStoreObjects
    public abstract class DataStoreObject<T>
    {
        public XElement CreateXElement(T element)
        {
            var rootElement = new XElement(typeof(T).Name);

            var properties = new List<Expression<Func<T, object>>>();
            SetProperties(properties);

            foreach (var item in properties)
            {
                // Get content elements
                var contentElement = item.Compile().Invoke(element);
                if (contentElement != null)
                {
                    Type type = contentElement.GetType();

                    // Check content is a IEnumerable
                    if (type.IsGenericType && contentElement is IEnumerable)
                    {
                        var contentElements = (IEnumerable)contentElement;
                        var xElementLists = DataStoreHelper.CreateXElementListFromIEnumerable(contentElements);

                        // Only add a XElement, when it has content
                        if (xElementLists.Any())
                        {
                            var listXElement = new XElement(StaticReflection.GetMemberName(item));
                            xElementLists.ToList().ForEach(e => listXElement.Add(e));
                            rootElement.Add(listXElement);
                        }
                    }
                    else
                    {
                        var subClass = DataStoreHelper.FindFirstSubClass(type);
                        // Check subClass exists so it is an advanced class
                        if (subClass == null)
                        {
                            rootElement.Add(new XElement(StaticReflection.GetMemberName(item), contentElement));
                        }
                        else
                        {
                            //var test = DataStoreHelper.CreateXElement(subClass, contentElement);
                            // TODO: CreateXElement with using the subClass CreateXElement
                            rootElement.Add(new XElement(StaticReflection.GetMemberName(item), contentElement));
                        }
                    }
                } 
            }

            return rootElement;
        }

        protected abstract void SetProperties(List<Expression<Func<T, object>>> selector);
    }
}
