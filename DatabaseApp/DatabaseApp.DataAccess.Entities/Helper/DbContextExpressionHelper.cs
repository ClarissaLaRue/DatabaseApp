using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Database.DataAccess.Entities.Interfaces;

namespace Database.Common.Helper
{
    public static class DbContextExpressionHelper
    {
        public static IEnumerable<PropertyInfo> GetFkProperties(Type type)
        {
            return type.GetProperties().Where(x => typeof(IEntity<int>).IsAssignableFrom(x.PropertyType));
        }

        public static IEnumerable<PropertyInfo> GetManyToOneProperties(Type type)
        {
            return
                type.GetProperties().Where(
                    x =>
                        x.PropertyType.IsGenericType
                        && x.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>)
                        && typeof(IEntity<int>).IsAssignableFrom(x.PropertyType.GetGenericArguments()[0])
                );
        }

        public static PropertyInfo GetManyToOneProperty(Type type, Type refType)
        {
            Type propertyType = typeof(ICollection<>).MakeGenericType(refType);
            return type.GetProperties().FirstOrDefault(x => propertyType.IsAssignableFrom(x.PropertyType));
        }
    }
}