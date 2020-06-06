using System;
using System.Linq;
using System.Reflection;
using Database.Common.Helper;

namespace Database.DataAccess.Entities.Helper
{
public static class DynamicHelper
    {
        public static object InvokeDynamic(string methodName, object target, params object[] args)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            var methodInfo = new object();

            var methods = target.GetType().GetMethods().Where(m => m.Name == methodName).ToList();
            if (methods.Count == 1)
            {
                methodInfo = methods.First();
            }
            else
            {
                foreach (var method in methods.Where(method => IsMatch(method, args)))
                {
                    methodInfo = method;
                }
            }

            if (methodInfo != null)
            {
                var method = (MethodInfo)methodInfo;
                return method.Invoke(target, args);
            }
            throw new InvalidOperationException(string.Format("Method {0} was not found on type {1}", methodName, target));
        }


        public static object InvokeGenericMethod(string methodName, object target, Type entityType, PropertyInfo propertyInfo = null)
        {
            if (propertyInfo != null)
            {
                var methodInfo = target.GetType().GetMethod(methodName)?.MakeGenericMethod(new[] { propertyInfo.PropertyType });

                if (methodInfo != null)
                {
                    return methodInfo.Invoke(target, new object[] { ExpressionHelper.GetPropertyExpression(propertyInfo) });
                }
            }
            else
            {
                var methods = target.GetType().GetMethods().Where(m => m.Name == methodName && !m.GetParameters().Any());
                var methodInfo = methods.First();

                if (methodInfo != null)
                {
                    return methodInfo.Invoke(target, new object[] { });
                }
            }

            throw new InvalidOperationException(string.Format("Method {0} was not found on type {1}", methodName, target));
        }
        
        public static object InvokeGenericWithType(string methodName, object target, Type entityType, PropertyInfo propertyInfo)
        {
            var method = target.GetType().GetMethods().FirstOrDefault(m => m.Name == methodName && m.GetParameters().Any() && !m.IsGenericMethodDefinition);

            if (method != null)
            {
                return method.Invoke(target, new object[] { ExpressionHelper.GetPropertyExpression(propertyInfo) });
            }
            var methodInfo = target.GetType().GetMethod(methodName)?.MakeGenericMethod(new Type[] { TypeHelper.GetItemType(propertyInfo.PropertyType) });
            if (methodInfo != null)
            {
                return methodInfo.Invoke(target, new object[] { ExpressionHelper.GetPropertyExpression(propertyInfo) });
            }
            throw new InvalidOperationException(string.Format("Method {0} was not found on type {1}", methodName, target));
        }

        public static bool IsMatch(MethodInfo methodInfo, object[] args)
        {
            var parameters = methodInfo.GetParameters();
            if (args.Count() == parameters.Count())
            {
                for (int i = 0; i < args.Count(); i++)
                {
                    var paramType = parameters[i].ParameterType;
                    var argType = args[0].GetType();

                    if (!paramType.IsAssignableFrom(argType))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}