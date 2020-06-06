using System;
using Database.DataAccess.Entities.Interfaces;

namespace Database.DataAccess.Entities.Base
{
    public class AuditableEntityBase<T> : EntityBase<T>, IAuditable
    {
        /// <summary>
        /// This field contains the date and time of the creation of the object.
        /// </summary>

        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// This field contains the date and time of the change of the object.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}