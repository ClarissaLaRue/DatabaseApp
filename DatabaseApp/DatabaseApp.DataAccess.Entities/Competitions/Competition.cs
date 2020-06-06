using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Database.DataAccess.Entities.Base;
using Database.DataAccess.Entities.Building;
using Database.DataAccess.Entities.Competitions.Members;
using Database.DataAccess.Entities.Sports;

namespace Database.DataAccess.Entities.Competitions
{
    public class Competition : EntityBase<int>
    {
        [NotNull]
        public string Name { get; set; }
        
        [NotNull]
        public string Organizers { get; set; }
        
        [NotNull]
        public DateTime EventDate { get; set; }
        
        public int SportID { get; set; }
        [NotNull]
        public virtual Sport Sport { get; set; }
        
        public int BuidingID { get; set; }
        [NotNull]
        public virtual SportBuilding Building { get; set; }
        
        public virtual ICollection<Member> Members { get; set; }
        
    }
}