using System;
using System.Collections.Generic;
using SportManager.DataAccess.Entities.Building;
using SportManager.DataAccess.Entities.Competitions.Members;
using SportManager.Models.Base;
using SportManager.Models.Model.Sport;

namespace SportManager.Models.Model.Competition
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