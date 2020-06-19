using Database.Models.Model.Sport;
using Database.Models.ViewModel.Base;

namespace Database.Models.Model.Competition
{
    public class MemberModel : ModelBase
    {
        public int Place { get; set; }
        public CompetitionModel Competition { get; set; }
        public SportModel SportMan { get; set; }
    }
}