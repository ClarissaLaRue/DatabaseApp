using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SportManager.DataAccess.Repositories.Base.Interfaces
{
    public interface ISqlRepository<T> : IRepository<T>
    {
        #region Methods

        bool IsRemoved(T item);

        bool IsDirty(T item);

        bool IsNew(T item);

        void Refresh(T item);

        void ReloadNavigationProperty<TElement>(T item, Expression<Func<T, ICollection<TElement>>> navigationProperty) where TElement : class;

        void Add(T item);

        void Flush();

        #endregion
    }
}