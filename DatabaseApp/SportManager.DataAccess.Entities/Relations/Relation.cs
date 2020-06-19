using Database.DataAccess.Entities.Trainers;
using SportManager.DataAccess.Entities.Base;
using SportManager.DataAccess.Entities.SportsMans;

namespace SportManager.DataAccess.Entities.Relations
{
    public class Relation : EntityBase<int>
    {
        public int SportManID { get; set; }
        [NotNull]
        public virtual SportMan SportMan { get; set; }
        public int TrainerID { get; set; }
        [NotNull]
        public virtual Trainer Trainer { get; set; }
    }
}