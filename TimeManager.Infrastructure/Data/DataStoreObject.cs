﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using TimeManager.Infrastructure.Utils;

namespace TimeManager.Infrastructure.Data
{
    // TODO: Add a static caching of the used DataStoreObjects
    public abstract class DataStoreObject<T> where T : new()
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
            T obj = new T();
            var objProperties = obj.GetType().GetProperties();

            foreach (var elementProp in element.Elements())
            {
                // TODO: check property contains list or other ref type
                // then resolve this other ref type by using the appropriate DataStoreObject<T>
                var objProp = objProperties.FirstOrDefault(p => p.Name == elementProp.Name);

                // Reference type
                if (objProp != null)
                {
                    var propType = objProp.GetType();
                    var storeClass = DataStoreHelper.FindFirstStoreClass(propType);
                    if (storeClass != null)
                    {
                        // TODO: solve by using store class
                        var classInstance = Activator.CreateInstance(storeClass);
                        MethodInfo createObject = storeClass.GetMethod("CreateObject");
                        if (createObject != null)
                        {
                            return (T)createObject.Invoke(classInstance, new object[] { elementProp.Value });
                        }
                    }
                    else
                    {
                        // TODO: check is list
                        // Try to parse
                        objProp.SetValue(obj, elementProp.Value);
                    }
                }
            }

            return obj;
        }

        protected abstract void SetProperties(List<Expression<Func<T, object>>> selector);
    }
}
