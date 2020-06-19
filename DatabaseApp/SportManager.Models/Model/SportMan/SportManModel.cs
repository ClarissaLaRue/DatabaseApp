using SportManager.Models.Base;
using SportManager.Models.Model.Sport;
using SportManager.Models.Model.SportClub;

namespace SportManager.Models.Model.SportMan
{
    public class SportManModel : ModelBase
    {
        public string Name { get; set; }
        public SportClubModel SportClub { get; set; }
        public string SportCategory { get; set; }
        public SportModel Sport { get; set; }
    }
}