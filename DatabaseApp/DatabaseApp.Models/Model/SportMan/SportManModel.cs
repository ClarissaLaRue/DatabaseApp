using Database.Models.Model.Sport;
using Database.Models.Model.SportClub;
using Database.Models.ViewModel.Base;

namespace Database.Models.Model.SportMan
{
    public class SportManModel : ModelBase
    {
        public string Name { get; set; }
        public SportClubModel SportClub { get; set; }
        public string SportCategory { get; set; }
        public SportModel Sport { get; set; }
    }
}