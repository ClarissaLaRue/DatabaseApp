using System.Diagnostics.CodeAnalysis;
using Database.DataAccess.Entities.Base;
using Database.DataAccess.Entities.SportsMans;
using Database.DataAccess.Entities.Trainers;

namespace Database.DataAccess.Entities.Relations
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