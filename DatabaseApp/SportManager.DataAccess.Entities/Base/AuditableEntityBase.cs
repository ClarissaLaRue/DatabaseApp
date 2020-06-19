using System;
using SportManager.DataAccess.Entities.Interfaces;

namespace SportManager.DataAccess.Entities.Base
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