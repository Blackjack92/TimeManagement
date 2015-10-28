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
                        if (elementProp.HasElements)
                        {
                            var list = objProp.GetValue(obj) as IList;
                            // IsList, add elements to existing list
                            foreach (var item in elementProp.Elements())
                            {
                                Type type = null;
                                foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                                {
                                    if (a.GetTypes().Any(ty => ty.Name == item.Name.LocalName))
                                    {
                                        type = a.GetTypes().FirstOrDefault(ty => ty.Name == item.Name.LocalName);
                                        break;
                                    }
                                }


                                //var type =  Type.GetType(item.Name.LocalName);

                                var test = DataStoreHelper.FindFirstStoreClass(type);
                                if (test != null)
                                {
                                    // TODO: solve by using store class
                                    var classInstance = Activator.CreateInstance(test);
                                    MethodInfo createObject = test.GetMethod("CreateObject");
                                    if (createObject != null)
                                    {
                                        list.Add(createObject.Invoke(classInstance, new object[] { item }));
                                    }
                                }
                                else
                                {
                                    list.Add(Activator.CreateInstance(type));
                                }
                            }
                        }
                        else
                        {
                            var bla = objProp.PropertyType;
                            // Try to parse
                            if (bla == typeof(string))
                            {
                                objProp.SetValue(obj, elementProp.Value);
                            }
                            else
                            {
                                var test = Activator.CreateInstance(bla);

                                MethodInfo mi = bla.GetMethod("Parse", new Type[] { typeof(string) });
                                var value = mi.Invoke(null, new object[] { elementProp.Value });
                                objProp.SetValue(obj, value);
                            }
                           
                        }
                    }
                }
            }

            return obj;
        }

        protected abstract void SetProperties(List<Expression<Func<T, object>>> selector);
    }
}
