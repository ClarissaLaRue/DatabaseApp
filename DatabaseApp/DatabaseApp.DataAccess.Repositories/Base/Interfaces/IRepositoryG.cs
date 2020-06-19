using System.Linq;

namespace Database.DataAccess.Repositories.Base.Interfaces
{
    public interface IRepository<T> : IRepository, IQueryable<T>
    {
        void Remove(T item);

        void Remove(IQueryable<T> items);

        void Deactivate(T item);

        void Deactivate(IQueryable<T> items);

        void Save(T item);

        T CreateNew();

        new IQueryable<T> Items(bool includeInactive);

        new IQueryable<T> Items();
    }
}