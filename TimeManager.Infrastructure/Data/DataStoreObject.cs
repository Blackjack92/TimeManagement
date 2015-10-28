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
                        var storeClass = DataStoreHelper.FindFirstStoreClass(type);
                        // Check storeClass exists so it is an advanced class
                        if (storeClass == null)
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

        public T CreateObject(XElement element)
        {
            T obj = Activator.CreateInstance<T>();
            var objProperties = obj.GetType().GetProperties();

            // Iterate all properties
            foreach (var elementProp in element.Elements())
            {
                // Check of the object does contain such a property
                var objProp = objProperties.FirstOrDefault(p => p.Name == elementProp.Name);

                if (objProp != null)
                {
                    // Depending on the propertytype the value has to be loaded correct
                    var propType = objProp.GetType();
                    var storeClass = DataStoreHelper.FindFirstStoreClass(propType);
                    if (DataStoreHelper.IsDataStoreClass(propType))
                    {
                        // Reference type
                        var createdObject = DataStoreHelper.CreateObject(storeClass, new object[] { elementProp.Value });
                        return (T)createdObject;
                    }
                    else
                    {
                        // Handle list
                        if (elementProp.HasElements)
                        {
                            var list = objProp.GetValue(obj) as IList;
                            // IsList, add elements to existing list
                            foreach (var item in elementProp.Elements())
                            {
                                Type type = StaticReflection.GetTypeOfString(item.Name.LocalName);

                                // Check if the list contains of DataStoreObjects<T>
                                var listStoreClass = DataStoreHelper.FindFirstStoreClass(type);
                                if (listStoreClass != null)
                                {
                                    var createdObject = DataStoreHelper.CreateObject(listStoreClass, new object[] { item });
                                    list.Add(createdObject);
                                }
                                else
                                {
                                    list.Add(Activator.CreateInstance(type));
                                }
                            }
                        }
                        else
                        {
                            // Handle single element
                            var propertyType = objProp.PropertyType;
                            var value = ParseToPropertyType(propertyType, elementProp.Value);
                            objProp.SetValue(obj, value);
                        }
                    }
                }
            }

            return obj;
        }

        private object ParseToPropertyType(Type propertyType, object value)
        {
            try
            {
                return Convert.ChangeType(value, propertyType);
            }
            catch (InvalidCastException)
            {
                return StaticReflection.ParseToObject(propertyType, value);
            }
        }

        protected abstract void SetProperties(List<Expression<Func<T, object>>> selector);
    }
}
