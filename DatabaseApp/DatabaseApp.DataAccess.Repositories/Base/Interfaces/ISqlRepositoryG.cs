using Database.DataAccess.Entities.Interfaces;

namespace Database.DataAccess.Repositories.Base.Interfaces
{
    public interface ISqlRepository<T, in TId> : ISqlRepository<T> where T:IEntity<TId>
    {
        T Get(TId id);
    }
}