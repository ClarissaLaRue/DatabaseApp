using System;
using Database.Common.Interfaces;

namespace Database.DataAccess.Entities.Interfaces
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class InitializeAttribute : Attribute
    {
        public object[] Parameters { get; private set; }

        public Type ObjectType { get; set; }

        public Type FactoryType { get; set; }

        public InitializeAttribute(params object[] parameters)
        {
            this.Parameters = parameters;
        }

        public virtual object Init(Type propertyType, object value)
        {
            if (this.FactoryType != null)
            {
                var factory = (IFactory)Activator.CreateInstance(this.FactoryType);
                return factory.CreateInstance(propertyType, this.Parameters);
            }
            if (this.ObjectType != null)
            {
                propertyType = this.ObjectType;
            }
            return Activator.CreateInstance(propertyType, this.Parameters);
        }
    }
}