using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Common.Helper
{
    public class TypeHelper
    {

        private static readonly Dictionary<Type, string> _PrimitiveTypesMap = new Dictionary<Type, string>()
            {
                { typeof(int), "int" },
                { typeof(uint), "uint" },
                { typeof(short), "short" },
                { typeof(ushort), "short" },
                { typeof(byte), "byte" },
                { typeof(char), "char" },
                { typeof(string), "string" },
                { typeof(long), "long" },
                { typeof(ulong), "ulong" },
                { typeof(float), "float" },
                { typeof(double), "double" },
                { typeof(bool), "bool" },
            };


        public static bool IsSimpleType(Type type)
        {
            return
                type.IsPrimitive
                || type == typeof(string)
                || (type.IsGenericType
                    && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                    && IsSimpleType(type.GetGenericArguments()[0]));
        }

        public static Type GetItemType(Type enumerableType)
        {
            if (enumerableType.IsArray)
            {
                return enumerableType.GetElementType();
            }
            var enumerableGenericInterfaceType = enumerableType.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            if (enumerableGenericInterfaceType != null)
            {
                return enumerableGenericInterfaceType.GetGenericArguments()[0];
            }
            return typeof(object); //TODO: Not sure about this logic
        }

        public static string GetTypeName(Type type, bool includeNamespace)
        {
            string name;

            if (_PrimitiveTypesMap.TryGetValue(type, out name)) return name;

            name = "";

            if (type.IsNested)
            {
                name = GetTypeName(type.DeclaringType, includeNamespace);
            }
            else if (includeNamespace)
            {
                name = type.Namespace;
            }

            if (!string.IsNullOrEmpty(name)) name += ".";

            if (type.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();

                if (genericType == typeof(Nullable<>))
                {
                    return string.Format("{0}?", GetTypeName(type.GetGenericArguments()[0]));
                }

                name += genericType.Name;

                var genericTypeParamsSb = new StringBuilder();
                foreach (var t in type.GetGenericArguments())
                {
                    genericTypeParamsSb.AppendFormat("{0}, ", GetTypeName(t, includeNamespace));
                }
                string genericTypeParams = genericTypeParamsSb.ToString();
                name += string.Format("<{0}>", genericTypeParams.Remove(genericTypeParams.Length - 2));
            }
            else
            {
                name += type.Name;
            }

            return name;
        }

        public static string GetTypeName(Type type)
        {
            return GetTypeName(type, false);
        }
    }
}