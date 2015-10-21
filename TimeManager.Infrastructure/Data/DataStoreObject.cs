using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                // Get content elements
                var contentElement = item.Compile().Invoke(element);
                if (contentElement == null) { return null; }

                Type type = contentElement.GetType();

                // Check content is a IEnumerable
                if (type.IsGenericType && contentElement is IEnumerable)
                {
                    var contentElements = (IEnumerable)contentElement;
                    var listXElement = new XElement(StaticReflection.GetMemberName(item));
                    CreateXElementListFromIEnumerable(contentElements).ToList().ForEach(e => listXElement.Add(e));
                    rootElement.Add(listXElement);
                }
                else
                {
                    var subClass = FindFirstSubClass(type);
                    // Check subClass exists so it is an advanced class
                    if (subClass == null)
                    {
                        rootElement.Add(new XElement(StaticReflection.GetMemberName(item), contentElement));
                    }
                    else
                    {
                        // TODO: CreateXElement
                        rootElement.Add(new XElement(StaticReflection.GetMemberName(item), contentElement));
                    }
                }
            }

            return rootElement;
        }

        private static Type FindFirstSubClass(Type genericType)
        {
            // Create generic class DataStoreObject<T> where T is the type of the content element
            Type genericClass = typeof(DataStoreObject<>);
            Type baseClass = genericClass.MakeGenericType(genericType);

            // Find derived classes from the DataStoreObject<T> where T is the type of the content element
            MethodInfo getEnumerableOfType = typeof(StaticReflection).GetMethod("GetEnumerableOfType").MakeGenericMethod(new Type[] { baseClass });
            var subClasses = getEnumerableOfType.Invoke(null, null);

            // Return first found subClass
            if (subClasses is IEnumerable)
            {
                foreach (var subClass in (IEnumerable)subClasses)
                {
                    return subClass as Type;
                }
            }

            return null;
        }

        private static XElement CreateXElement(Type classType, object content)
        {
            // Create a derived DataStoreObject<T> object where T is the type of the content element
            object classInstance = Activator.CreateInstance(classType, null);

            // Invoke CreateXElement from the derived DataStoreObject
            MethodInfo createXElement = classType.GetMethod("CreateXElement");
            if (createXElement != null)
            {
                // Store created xElement in the rootElement
                return (XElement) createXElement.Invoke(classInstance, new object[] { content });
            }

            return null;
        }

        private static IEnumerable<XElement> CreateXElementListFromIEnumerable(IEnumerable contentElements)
        {
            var listXElements = new List<XElement>();
            // Iterate each content element
            foreach (var content in contentElements)
            {
                var subClassType = FindFirstSubClass(content.GetType());
                if (subClassType != null)
                {
                    XElement xElement = CreateXElement(subClassType, content);
                    if (xElement != null)
                    {
                        listXElements.Add(xElement);
                    }
                }
                else
                {
                    throw new DataStoreException("No derived class for DataStoreObject<T> was found.");
                }
            }
            return listXElements;
        }

        protected abstract void SetProperties(List<Expression<Func<T, object>>> selector);
    }
}
