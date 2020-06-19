using SportManager.DataAccess.Entities.Interfaces;

namespace SportManager.DataAccess.Entities.Base
{
    public class EntityBase<T> : ModelBase, IEntity<T>
    {
        
        [Key]
        [NotNull]
        public virtual T ID { get; set; }
    }
}