using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SportManager.DataAccess.Repositories.Helper
{
    public static class EntityFieldHelper
    {
        public static Expression<Func<T, bool>> BuildKeyEqualExpression<T>(T entity)
        {
            return BuildKeyEqualExpression<T>(entity, typeof (KeyAttribute));
        }

        public static Expression<Func<T, bool>> BuildKeyEqualExpression<T>(T entity, Type keyAttributeType)
        {
            Type type = typeof (T);
            var entityParam = Expression.Parameter(type, "entity");

            Expression resBody = null;

            foreach (var propertyInfo in GetKeyProperties(type, keyAttributeType))
            {
                var memberExpr = Expression.MakeMemberAccess(entityParam, propertyInfo);
                var keyValue = propertyInfo.GetValue(entity, null);
                var constExpr = Expression.Constant(keyValue);
                var equalExpr = Expression.Equal(memberExpr, constExpr);
                resBody = resBody == null ? Expression.And(resBody, equalExpr) : equalExpr;
            }

            return (Expression<Func<T, bool>>)Expression.Lambda(resBody, entityParam);
        }

        public static List<PropertyInfo> GetKeyProperties(Type entityType, Type keyAttributeType)
        {
            var keyProperties = from p in entityType.GetProperties()
                                let attr = p.GetCustomAttributes(keyAttributeType, true)
                                where attr != null && attr.Length != 0
                                select p;

            return keyProperties.ToList();
        }

        public static object[] GetKeyValues<T>(T entity)
        {
            return GetKeyValues(entity, typeof (KeyAttribute));
        }

        public static object[] GetKeyValues<T>(T entity, Type keyAttributeType)
        {
            var keyProperties = GetKeyProperties(typeof(T), keyAttributeType);

            object[] res = new object[keyProperties.Count];
            int i = 0;

            foreach (var propertyInfo in keyProperties)
            {
                res[i] = propertyInfo.GetValue(entity, null);
                ++i;
            }

            return res;
        }

        public static void CopyPropertiesFromFirstToSecond<T>(T first, T second, Func<PropertyInfo, bool> propertyFilter)
        {
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties().Where(propertyFilter))
            {
                propertyInfo.SetValue(second, propertyInfo.GetValue(first, null), null);
            }
        }
    }
}