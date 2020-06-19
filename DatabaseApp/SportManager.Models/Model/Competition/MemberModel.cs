using SportManager.Models.Base;
using SportManager.Models.Model.Sport;

namespace SportManager.Models.Model.Competition
{
    public class MemberModel : ModelBase
    {
        public int Place { get; set; }
        public CompetitionModel Competition { get; set; }
        public SportModel SportMan { get; set; }
    }
}