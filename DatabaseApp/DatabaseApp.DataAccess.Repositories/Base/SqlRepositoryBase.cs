using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Database.Common.Helper;
using Database.DataAccess.Entities.Base;
using Database.DataAccess.Entities.Interfaces;
using Ninject;

namespace Database.DataAccess.Repositories.Base
{
    public class SqlRepositoryBase<T> : ISqlRepository<T>
        where T : class, IEntity
    {
        #region Fields

        private DbContextBase _context;
        private readonly Expression<Func<T, bool>> _isNotInactiveExpression;
        private readonly Expression<Func<T, int>> _sortBySequenceExpression;

        #endregion

        protected DbContextBase Context
        {
            get
            {
                if (_context != null && _context.IsDisposed)
                {
                    NinjectHelper.Kernel.Release(_context);
                    _context = null;
                }

                if (_context == null)
                {
                    _context = NinjectHelper.Get<DbContextBase>();
                    if (_context.IsDisposed)
                    {
                        throw new InvalidOperationException("This should not happen!!!");
                    }
                }

                return _context;
            }
        }

        #region Constructors

        [Inject]
        public SqlRepositoryBase()
        {
            if (typeof(IInactivatable).IsAssignableFrom(typeof(T)))
            {
                var xParam = Expression.Parameter(typeof(T), "x");
                _isNotInactiveExpression = (Expression<Func<T, bool>>)Expression.Lambda(
                    typeof(Func<T, bool>),
                    Expression.Not(Expression.MakeMemberAccess(xParam, typeof(T).GetProperty("IsInactive"))),
                    xParam);
                //_IsInactiveExpression
            }

            if (typeof(ISorted).IsAssignableFrom(typeof(T)))
            {
                var xParam = Expression.Parameter(typeof(T), "x");
                _sortBySequenceExpression = (Expression<Func<T, int>>)Expression.Lambda(
                    typeof(Func<T, int>),
                    Expression.MakeMemberAccess(xParam, typeof(T).GetProperty("Sequence")),
                    xParam);
                //_IsInactiveExpression
            }
        }

        public SqlRepositoryBase(DbContextBase context): this()
        {
            _context = context;// ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion //Constructors

        //private DbSet<T> _dbSet;

        protected virtual DbSet<T> GetDbSet()
        {
            return this.Context.GetDbSet<T>();// _dbSet ?? (_dbSet = this.Context.GetDbSet<T>());
        }

        #region ISqlRepository<T> Members

        public virtual bool IsRemoved(T item)
        {
            var state = this.Context.Entry(item).State;
            return (state == EntityState.Deleted);
        }

        public virtual bool IsDirty(T item)
        {
            var state = this.Context.Entry(item).State;
            return (state == EntityState.Added || state == EntityState.Modified);
        }

        public virtual bool IsNew(T item)
        {
            return (this.Context.Entry(item).State == EntityState.Added);
        }

        public virtual void Refresh(T item)
        {
            this.Context.Entry(item).Reload();
        }

        public void ReloadNavigationProperty<TElement>(T item,
            Expression<Func<T, ICollection<TElement>>> navigationProperty)
            where TElement : class
        {
            this.Context.Entry(item).Collection<TElement>(navigationProperty).Query();
        }

        public virtual void Add(T item)
        {
            GetDbSet().Add(item);
        }

        public void Flush()
        {
            this.Context.SaveChanges();
        }

        #endregion

        #region IRepository<T> Members

        /// <summary>
        /// Marks entities as deleted in repository, must call Flush to permanently delete
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(T item)
        {
            var dbSet = GetDbSet();
            dbSet.Remove(item);
        }

        /// <summary>
        /// Marks entities as deleted in repository, must call Flush to permanently delete
        /// </summary>
        /// <param name="items"></param>
        public virtual void Remove(IQueryable<T> items)
        {
            var dbSet = GetDbSet();
            items.ToList().ForEach(x => dbSet.Remove(x));
        }

        public virtual void Deactivate(T item)
        {
            if (!(item is IInactivatable))
            {
                throw new InvalidOperationException(string.Format("{0} doesn't implement interface {1}", typeof(T).FullName, typeof(IInactivatable).FullName));
            }
            ((IInactivatable)item).IsInactive = true;
            Save(item);
        }

        public virtual void Deactivate(IQueryable<T> items)
        {
            if (!typeof(IInactivatable).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException(string.Format("{0} doesn't implement interface {1}", typeof(T).FullName, typeof(IInactivatable).FullName));
            }
            items.OfType<IInactivatable>().ToList().ForEach(x => x.IsInactive = true);
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Creates a new instance of entity, not that instance is not added to repository, to add it call Add or Save method
        /// </summary>
        /// <returns></returns>
        public virtual T CreateNew()
        {
            return GetDbSet().Create();
        }

        public virtual void Save(T item)
        {
            try
            {
                var dbSet = GetDbSet();

                if (this.Context.Entry(item).State != EntityState.Added)
                {
                    var existingEntity = dbSet.Find(EntityFieldHelper.GetKeyValues(item));

                    if (existingEntity != null)
                    {
                        if (existingEntity != item)
                        {
                            //TODO: Optimize
                            EntityFieldHelper.CopyPropertiesFromFirstToSecond(
                                item,
                                existingEntity,
                                x => (!x.PropertyType.IsClass || x.PropertyType == typeof(string)) && x.CanWrite && x.CanRead);
                        }
                    }
                    else
                    {
                        dbSet.Add(item);
                    }
                }

                this.Context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                        DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    outputLines.AddRange(eve.ValidationErrors.Select(ve => string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage)));
                }
                throw;
            }
        }

        public virtual IQueryable<T> Items()
        {
            IQueryable<T> query = GetDbSet();
            if (typeof(IInactivatable).IsAssignableFrom(typeof(T)))
            {
                query = query.Where(_isNotInactiveExpression);
            }

            if (typeof(ISorted).IsAssignableFrom(typeof(T)))
            {
                query = query.OrderBy(_sortBySequenceExpression);
            }

            return query;
        }

        public virtual IQueryable<T> Items(bool includeInactive)
        {
            if (!typeof(IInactivatable).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException(string.Format("{0} doesn't implement interface {1}", typeof(T).FullName, typeof(IInactivatable).FullName));
            }

            IQueryable<T> query = GetDbSet();

            if (!includeInactive)
            {
                query = query.Where(_isNotInactiveExpression);
            }

            if (typeof(ISorted).IsAssignableFrom(typeof(T)))
            {
                query = query.OrderBy(_sortBySequenceExpression);
            }

            return query;
        }

        #endregion

        #region IRepository Members

        void IRepository.Remove(object item)
        {
            this.Remove((T)item);
        }

        void IRepository.Save(object item)
        {
            this.Save((T)item);
        }

        IQueryable IRepository.Items()
        {
            return this.Items();
        }

        IQueryable IRepository.Items(bool includeInactive)
        {
            return this.Items(includeInactive);
        }

        #endregion

        #region IEnumerable<T> Members

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            return this.Items().GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Items().GetEnumerator();
        }

        #endregion

        public virtual DbQuery<T> Include(string path)
        {
            return GetDbSet().Include(path);
        }

        #region IQueryable Members

        public Type ElementType
        {
            get { return this.Items().GetType(); }
        }

        public Expression Expression
        {
            get { return this.Items().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return this.Items().Provider; }
        }

        #endregion
    }

    public class SqlRepositoryBase<T, TId> : SqlRepositoryBase<T>, ISqlRepository<T, TId>
        where T : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        [Inject]
        public SqlRepositoryBase() : base()
        {
        }

        public SqlRepositoryBase(DbContextBase context) : base(context)
        {
        }

        public virtual T Get(TId id)
        {
            return Items().FirstOrDefault(i => i.ID.Equals(id));
        }
    }
}