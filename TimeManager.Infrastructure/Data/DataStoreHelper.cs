using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using TimeManager.Infrastructure.Utils;

namespace TimeManager.Infrastructure.Data
{
    public static class DataStoreHelper
    {
        public static Type FindFirstSubClass(Type genericType)
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

        public static XElement CreateXElement(Type classType, object content)
        {
            // Create a derived DataStoreObject<T> object where T is the type of the content element
            object classInstance = Activator.CreateInstance(classType, null);

            // Invoke CreateXElement from the derived DataStoreObject
            MethodInfo createXElement = classType.GetMethod("CreateXElement");
            if (createXElement != null)
            {
                // Store created xElement in the rootElement
                return (XElement)createXElement.Invoke(classInstance, new object[] { content });
            }

            return null;
        }

        public static IEnumerable<XElement> CreateXElementListFromIEnumerable(IEnumerable contentElements)
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

    }
}
