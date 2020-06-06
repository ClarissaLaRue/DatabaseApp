using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Database.DataAccess.Entities.Base;

namespace Database.DataAccess.Entities.Sports
{
    public class Sport : EntityBase<int>
    {
        [NotNull]
        [MaxLength(255)]
        public string SportName { get; set; } 
    }
}