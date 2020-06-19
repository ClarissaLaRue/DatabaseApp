using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SportManager.DataAccess.Entities.Base;
using SportManager.DataAccess.Entities.SportClub;
using SportManager.DataAccess.Entities.Sports;

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
        public virtual SportClub SportClub { get; set; }
    }
}