using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SportManager.Common.Helper
{
    public static class ExpressionHelper
    {
        public static Expression GetPropertyExpression(PropertyInfo propertyInfo, Type targetType=null)
        {
            var xParam = Expression.Parameter(targetType??propertyInfo.ReflectedType, "x");
            return Expression.Lambda(
                typeof(Func<,>).MakeGenericType(targetType??propertyInfo.ReflectedType, propertyInfo.PropertyType),
                Expression.Property(xParam, propertyInfo),
                xParam);
        }
    }
}