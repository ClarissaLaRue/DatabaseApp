using System.Collections.Generic;
using Database.Models.Model.Sport;
using Database.Models.Model.SportMan;
using Database.Models.Model.Trainer;
using Database.Models.ViewModel.Base;

namespace Database.Models.Model.SportClub
{
    public class SportClubModel : ModelBase
    {
        public string Name { get; set; }
        
        public SportModel Sport { get; set; }
        
        public List<SportManModel> SportMans { get; set; }
        
        public List<TrainerModel> Trainers { get; set; }
    }
}