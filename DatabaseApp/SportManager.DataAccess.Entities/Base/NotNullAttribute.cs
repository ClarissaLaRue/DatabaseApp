using System;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SportManager.DataAccess.Entities.Base
{
    public class NotNullAttribute : EfAttributeBase
    {
        #region Overriden methods

        public override void ApplyConfiguration<TEntityType>(EntityTypeConfiguration<TEntityType> entityTypeConfiguration, PropertyInfo targetProperty)
        {
            //if target property has primitive data type (e.g. int, tinyint, char, etc) - it is NOT NULL by default
            if (targetProperty.PropertyType.IsPrimitive && targetProperty.PropertyType.IsArray == false)
            {
                return;
            }
            var propertyMethods =
                entityTypeConfiguration.GetType().GetMethods().Where(m => m.Name == "Property").ToList();
            foreach (var propertyMethod in propertyMethods)
            {
                var parameters = propertyMethod.GetParameters();
                if (parameters.Count() == 1 && parameters[0].ParameterType.IsAssignableFrom(GetPropertyExpression(targetProperty, typeof(TEntityType)).GetType()))
                {
                    var expr = propertyMethod.Invoke(entityTypeConfiguration, new object[] { GetPropertyExpression(targetProperty, typeof(TEntityType)) });
                    expr.GetType().GetMethod("IsRequired").Invoke(expr, new object[] { });
                }
            }
        }

        public static LambdaExpression GetPropertyExpression(PropertyInfo propertyInfo, Type targetType = null)
        {
            var xParam = Expression.Parameter(targetType ?? propertyInfo.ReflectedType, "x");
            return Expression.Lambda(
                typeof(Func<,>).MakeGenericType(targetType ?? propertyInfo.ReflectedType, propertyInfo.PropertyType),
                Expression.Property(xParam, propertyInfo),
                xParam);
        }

        #endregion //Overriden methods
    }
}