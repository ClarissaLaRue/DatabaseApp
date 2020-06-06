using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Database.DataAccess.Entities.Base;

namespace Database.DataAccess.Entities.Building
{
    public class SportBuilding : EntityBase<int>
    {
        [NotNull]
        [MaxLength(255)]
        public string Name { get; set; }
        [NotNull]
        [MaxLength(255)]
        public string Type { get; set; }
        [NotNull]
        [MaxLength(255)]
        public string Address { get; set; }
    }
}