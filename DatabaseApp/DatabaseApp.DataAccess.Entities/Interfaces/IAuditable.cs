using System;

namespace Database.DataAccess.Entities.Interfaces
{
    public interface IAuditable
    {
        DateTime CreatedDate { get; set; }

        DateTime UpdatedDate { get; set; }
    }
}