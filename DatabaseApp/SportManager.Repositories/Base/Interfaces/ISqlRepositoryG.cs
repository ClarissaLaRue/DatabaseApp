using SportManager.DataAccess.Entities.Interfaces;

namespace SportManager.Repositories.Base.Interfaces
{
    public interface ISqlRepository<T, in TId> : ISqlRepository<T> where T:IEntity<TId>
    {
        T Get(TId id);
    }
}