using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.DataAccess.Entities.Trainers;
using SportManager.DataAccess.Entities.Base;
using SportManager.DataAccess.Entities.Sports;
using SportManager.DataAccess.Entities.SportsMans;

namespace SportManager.DataAccess.Entities.SportClub
{
    public class SportClub : EntityBase<int>
    {
        [NotNull]
        [MaxLength(255)]
        public string Name { get; set; }
        
        public int SportID { get; set; }
        
        [NotNull]
        public virtual Sport Sport { get; set; }
        
        public virtual ICollection<SportMan> SportsMans { get; set; }
        
        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}