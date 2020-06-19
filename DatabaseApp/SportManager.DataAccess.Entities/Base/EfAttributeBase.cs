using System;
using System.Data.Entity.ModelConfiguration;
using System.Reflection;

namespace SportManager.DataAccess.Entities.Base
{
    public abstract class EfAttributeBase : Attribute
    {
        
        public abstract void ApplyConfiguration<TEntityType>(
            EntityTypeConfiguration<TEntityType> entityTypeConfiguration, PropertyInfo targetProperty)
            where TEntityType : class;
    }
}