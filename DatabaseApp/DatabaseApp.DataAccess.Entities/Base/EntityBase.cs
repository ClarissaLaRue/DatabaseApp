using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Database.DataAccess.Entities.Interfaces;

namespace Database.DataAccess.Entities.Base
{
    public class EntityBase<T> : ModelBase, IEntity<T>
    {
        
        [Key]
        [NotNull]
        public virtual T ID { get; set; }
    }
}