using System.Diagnostics.CodeAnalysis;
using Database.DataAccess.Entities.Base;
using Database.DataAccess.Entities.SportsMans;

namespace Database.DataAccess.Entities.Competitions.Members
{
    public class Member : EntityBase<int>
    {
        [NotNull]
        public int Place { get; set; }
        
        public int CompetitionsID { get; set; }
        [NotNull]
        public virtual Competition Competition { get; set; }
        
        public int SportManID { get; set; }
        [NotNull]
        public virtual SportMan SportMan { get; set; }
    }
}