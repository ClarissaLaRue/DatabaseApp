using System.ComponentModel.DataAnnotations;
using SportManager.DataAccess.Entities.Base;

namespace SportManager.DataAccess.Entities.Building
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