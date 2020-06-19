using System.Collections.Generic;
using SportManager.Models.Base;
using SportManager.Models.Model.Sport;
using SportManager.Models.Model.SportMan;
using SportManager.Models.Model.Trainer;

namespace SportManager.Models.Model.SportClub
{
    public class SportClubModel : ModelBase
    {
        public string Name { get; set; }
        
        public SportModel Sport { get; set; }
        
        public List<SportManModel> SportMans { get; set; }
        
        public List<TrainerModel> Trainers { get; set; }
    }
}