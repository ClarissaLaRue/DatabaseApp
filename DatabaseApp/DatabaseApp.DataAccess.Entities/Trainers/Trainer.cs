using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Database.DataAccess.Entities.Base;
using Database.DataAccess.Entities.Sports;

namespace Database.DataAccess.Entities.Trainers
{
    public class Trainer : EntityBase<int>
    {
        [NotNull]
        [MaxLength(255)]
        public string Name { get; set; }
        
        public int? SportID { get; set; }
        
        public virtual Sport Sport { get; set; }
        
        public int SportClubID { get; set; }
        [NotNull]
        public virtual SportClub.SportClub SportClub { get; set; }
    }
}