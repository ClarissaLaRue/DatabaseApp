using System.Linq;

namespace SportManager.Repositories.Base.Interfaces
{
    public interface IRepository
    {
        void Remove(object item);

        void Save(object item);
        
        IQueryable Items();
        
        IQueryable Items(bool includeInactive);
    }
}