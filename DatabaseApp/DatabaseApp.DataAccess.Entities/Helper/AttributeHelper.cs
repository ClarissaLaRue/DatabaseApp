using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Database.DataAccess.Entities.Helper
{
    public static class AttributeHelper
    {
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this MemberInfo type)
            where TAttribute : Attribute
        {
            return (TAttribute[])type.GetCustomAttributes(typeof(TAttribute), true);
        }

        public static TAttribute GetAttribute<TAttribute>(this MemberInfo type)
            where TAttribute : Attribute
        {
            var attrs = GetAttributes<TAttribute>(type);
            if (attrs == null) return null;
            return attrs.FirstOrDefault();
        }

        public static IEnumerable<KeyValuePair<PropertyInfo, TAttribute>> GetPropertiesWithAttributes<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            return from p in type.GetProperties()
                let attrs = (TAttribute[]) p.GetCustomAttributes(typeof (TAttribute), true)
                where attrs != null && attrs.Length != 0
                from a in attrs
                select new KeyValuePair<PropertyInfo, TAttribute>(p, a);
        }
    }
}