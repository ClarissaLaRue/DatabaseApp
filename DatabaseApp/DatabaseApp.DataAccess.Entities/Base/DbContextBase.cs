using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Database.Common.Helper;
using Database.Common.Providers;
using Database.DataAccess.Entities.Helper;
using Database.DataAccess.Entities.Interfaces;

namespace Database.DataAccess.Entities.Base
{
    public class DbContextBase : DbContext
    {
        private IDateTimeProvider _dateTimeProvider;
        
        #region Constructors

        protected DbContextBase(string connectionString)
            : base(connectionString)
        {

        }

        #endregion //Constructors
        
        public bool IsDisposed { get; private set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this.IsDisposed = true;

            NinjectHelper.Kernel.Release(this);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var entityGenericMethod = ReflectionHelper.MethodOf(() => modelBuilder.Entity<object>()).GetGenericMethodDefinition(); 

            var existingRefTables = new List<string>();

            foreach (var propertyInfo in this.GetType().GetProperties())
            {
                if (!propertyInfo.PropertyType.IsGenericType || propertyInfo.PropertyType.GetGenericTypeDefinition() != typeof(DbSet<>)) continue;
                var entityType = propertyInfo.PropertyType.GetGenericArguments()[0];

                var entityMethod = entityGenericMethod.MakeGenericMethod(entityType);
                var e = entityMethod.Invoke(modelBuilder, new object[0]);
                DynamicHelper.InvokeDynamic("ToTable", e, entityType.Name);

                //Processing one-to-many relations:
                foreach (PropertyInfo fkProperty in DbContextExpressionHelper.GetFkProperties(entityType))
                {
                    var fkIdProperty = entityType.GetProperty(string.Format(DbbConstants.FkPropertyFormat, fkProperty.Name));
                    if (fkIdProperty == null)
                    {
                        continue;
                    }
                    var attributes = (SkipDefaultMappingAttribute[])fkIdProperty.GetCustomAttributes(typeof(SkipDefaultMappingAttribute), false);

                    if (attributes.Any())
                    {
                        continue;
                    }
                    var has = DynamicHelper.InvokeGenericMethod(fkProperty.GetAttributes<NotNullAttribute>().Any() ? "HasRequired" : "HasOptional", e, entityType, fkProperty);

                    var manyToOneProperty = DbContextExpressionHelper.GetManyToOneProperty(fkProperty.PropertyType, entityType);

                    var withMany = manyToOneProperty == null ? DynamicHelper.InvokeGenericMethod("WithMany", has, entityType) : DynamicHelper.InvokeGenericWithType("WithMany", has, entityType, manyToOneProperty);

                    CascadeDeleteAttribute cascadeDeleteAttribute = manyToOneProperty != null ? ((CascadeDeleteAttribute[])manyToOneProperty.GetCustomAttributes(typeof(CascadeDeleteAttribute), false)).FirstOrDefault() : null;

                    bool cascadeDelete = cascadeDeleteAttribute != null && cascadeDeleteAttribute.Value;
                    var cascade = DynamicHelper.InvokeGenericMethod("HasForeignKey", withMany, entityType, fkIdProperty);
                    DynamicHelper.InvokeDynamic("WillCascadeOnDelete", cascade, cascadeDelete);
                }

                //Processing many-to-many relations:
                foreach (PropertyInfo fkProperty in DbContextExpressionHelper.GetManyToOneProperties(entityType))
                {
                    Type refEntityType = fkProperty.PropertyType.GetGenericArguments()[0];

                    var manyToOneProperty = DbContextExpressionHelper.GetManyToOneProperty(refEntityType, entityType);

                    if (manyToOneProperty == null) continue;

                    var attributes = (SkipDefaultMappingAttribute[])fkProperty.GetCustomAttributes(typeof(SkipDefaultMappingAttribute), false);
                    var manyToOneAttributes = (SkipDefaultMappingAttribute[])manyToOneProperty.GetCustomAttributes(typeof(SkipDefaultMappingAttribute), false);

                    if (attributes.Any() || manyToOneAttributes.Any())
                    {
                        continue;
                    }

                    string refTableName = null;
                    refTableName = string.Format(DbbConstants.LinkTableNameFormat, refEntityType.Name, entityType.Name);
                    if (existingRefTables.Contains(refTableName)) continue;
                    refTableName = string.Format(DbbConstants.LinkTableNameFormat, entityType.Name, refEntityType.Name);
                    existingRefTables.Add(refTableName);

                    Action<ManyToManyAssociationMappingConfiguration> configurationAction = x => x.ToTable(refTableName).MapLeftKey(string.Format(DbbConstants.FkPropertyFormat, entityType.Name)).MapRightKey(string.Format(DbbConstants.FkPropertyFormat, refEntityType.Name));

                    var hasMany = DynamicHelper.InvokeGenericWithType("HasMany", e, entityType, fkProperty);
                    var withMany = DynamicHelper.InvokeGenericWithType("WithMany", hasMany, entityType, manyToOneProperty);
                    DynamicHelper.InvokeDynamic("Map", withMany, configurationAction);

                }

                foreach (var property in entityType.GetProperties().Where(x => x.PropertyType.IsClass == false || x.PropertyType == typeof(string) || x.PropertyType.IsArray == true))
                {
                    foreach (EfAttributeBase attr in (EfAttributeBase[])property.GetCustomAttributes(typeof(EfAttributeBase), false))
                    {
                        try
                        {
                            attr.GetType().GetMethod("ApplyConfiguration").MakeGenericMethod(entityType).Invoke(attr, new[] { e, property });
                        }
                        catch (Exception ex)
                        {
                            var errorMessage =
                                string.Format(
                                    "Exception of type {0} was thrown while trying to apply attribute {1} on property {2}.{3}",
                                    ex.GetType().FullName, attr.GetType().FullName, property.ReflectedType.FullName,
                                    property.Name);

                            throw new Exception(
                               errorMessage,
                               ex);
                        }
                    }
                }
            }
        }

        private IDateTimeProvider DateTimeProvider
        {
            get
            {
                if (_dateTimeProvider == null)
                {
                    _dateTimeProvider = NinjectHelper.Kernel == null ? new UtcDateTimeProvider() : NinjectHelper.Get<IDateTimeProvider>();
                }

                return _dateTimeProvider;
            }
        }

        public override int SaveChanges()
        {
            var utcNow = DateTimeProvider.UtcNow;

            var added = base.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            var modified = base.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);

            foreach (var entity in added.Where(e => e.Entity is IAuditable).Select(e => (IAuditable)e.Entity))
            {
                entity.CreatedDate = utcNow;
                entity.UpdatedDate = utcNow;
            }

            foreach (var entity in modified.Where(e => e.Entity is IAuditable).Select(e => (IAuditable)e.Entity))
            {
                entity.UpdatedDate = utcNow;
            }

            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Rollback();

                throw ex;
            }

        }

        public void Detach(object entity)
        {
            ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }

        private void Rollback()
        {
            var added = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added).Select(e => e.Entity);
            var modified = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).Select(e => e.Entity);
            var deleted = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted).Select(e => e.Entity);

            var entities = added.Union(modified).Union(deleted).ToList();

            entities.ForEach(Detach);
        }
    }
}