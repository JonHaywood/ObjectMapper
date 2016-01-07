﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectMapper
{
    public static class ReflectionUtils
    {
        /// <summary>
        /// Gets all properties for the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>Properties on object.</returns>
        public static PropertyInfo[] GetProperties(object obj)
        {
            return GetProperties(obj.GetType());
        }

        /// <summary>
        /// Get type properties, including properties of inherited interfaces
        /// http://stackoverflow.com/a/2444090/112100
        /// </summary>
        /// <param name="type">Type to get properties from.</param>
        /// <returns>Properties on object.</returns>
        public static PropertyInfo[] GetProperties(Type type)
        {
            if (type.IsInterface)
            {
                var propertyInfos = new List<PropertyInfo>();

                var considered = new List<Type>();
                var queue = new Queue<Type>();
                considered.Add(type);
                queue.Enqueue(type);
                while (queue.Count > 0)
                {
                    var subType = queue.Dequeue();
                    foreach (var subInterface in subType.GetInterfaces())
                    {
                        if (considered.Contains(subInterface)) continue;

                        considered.Add(subInterface);
                        queue.Enqueue(subInterface);
                    }

                    var typeProperties = subType.GetProperties(
                        BindingFlags.FlattenHierarchy
                        | BindingFlags.Public
                        | BindingFlags.Instance);

                    var newPropertyInfos = typeProperties
                        .Where(x => !propertyInfos.Contains(x));

                    propertyInfos.InsertRange(0, newPropertyInfos);
                }

                return propertyInfos.ToArray();
            }

            return type.GetProperties(BindingFlags.FlattenHierarchy
                | BindingFlags.Public | BindingFlags.Instance);
        }
    }
}
