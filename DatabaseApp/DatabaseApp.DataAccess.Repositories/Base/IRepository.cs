using System.Linq;

namespace Database.DataAccess.Repositories.Base
{
    public interface IRepository
    {
        void Remove(object item);

        void Save(object item);
        
        IQueryable Items();
        
        IQueryable Items(bool includeInactive);
    }
}