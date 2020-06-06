using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Database.DataAccess.Entities.Base;
using Database.DataAccess.Entities.Sports;
using Database.DataAccess.Entities.SportsMans;
using Database.DataAccess.Entities.Trainers;

namespace Database.DataAccess.Entities.SportClub
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