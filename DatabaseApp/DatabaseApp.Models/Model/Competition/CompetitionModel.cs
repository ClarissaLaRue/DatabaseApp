using System;
using System.Collections.Generic;
using Database.DataAccess.Entities.Building;
using Database.DataAccess.Entities.Competitions.Members;
using Database.Models.Model.Sport;
using Database.Models.ViewModel.Base;

namespace Database.Models.Model.Competition
{
    public class CompetitionModel : ModelBase
    {
        public string Name { get; set; }
        
        public string Organizers { get; set; }
        
        public DateTime EventDate { get; set; }
        
        public SportModel Sport { get; set; }
        
        public SportBuilding Building { get; set; }
        
        public ICollection<Member> Members { get; set; }
    }
}