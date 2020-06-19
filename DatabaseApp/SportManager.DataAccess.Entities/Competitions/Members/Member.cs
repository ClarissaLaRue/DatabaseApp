using SportManager.DataAccess.Entities.Base;
using SportManager.DataAccess.Entities.SportsMans;

namespace SportManager.DataAccess.Entities.Competitions.Members
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