using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SportManager.Common.Helper
{
    public static class ReflectionHelper
    {
        #region Public methods

        public static MethodInfo MethodOf<TResult>(Expression<Func<TResult>> methodExpression)
        {
            return ((MethodCallExpression)methodExpression.Body).Method;
        }
        
        public static MethodInfo MethodOf(Expression<Action> methodExpression)
        {
            return ((MethodCallExpression)methodExpression.Body).Method;
        }

        public static MethodInfo MethodOf<TInstance, TResult>(Expression<Func<TInstance, TResult>> methodExpression)
        {
            return ((MethodCallExpression)methodExpression.Body).Method;
        }

        public static MethodInfo MethodOf<TInstance>(Expression<Action<TInstance>> methodExpression)
        {
            return ((MethodCallExpression)methodExpression.Body).Method;
        }

        public static PropertyInfo PropertyOf<TProperty>(Expression<Func<TProperty>> propertyGetExpression)
        {
            return ((MemberExpression)propertyGetExpression.Body).Member as PropertyInfo;
        }

        public static PropertyInfo PropertyOf<TInstance, TProperty>(Expression<Func<TInstance, TProperty>> propertyGetExpression)
        {
            return ((MemberExpression)propertyGetExpression.Body).Member as PropertyInfo;
        }

        public static FieldInfo FieldsOf<TProperty>(Expression<Func<TProperty>> fieldAccessExpression)
        {
            return ((MemberExpression)fieldAccessExpression.Body).Member as FieldInfo;
        }


        #endregion //Public methods
    }
}