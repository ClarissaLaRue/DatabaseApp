using Database.Models.Model.Sport;
using Database.Models.Model.SportClub;
using Database.Models.ViewModel.Base;

namespace Database.Models.Model.Trainer
{
    public class TrainerModel : ModelBase
    {
        public string Name { get; set; }
        
        public SportModel Sport { get; set; }
        
        public SportClubModel SportClub { get; set; }
    }
    
}