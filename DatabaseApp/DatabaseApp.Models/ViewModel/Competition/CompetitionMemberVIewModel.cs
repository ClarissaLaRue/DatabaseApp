using System.Collections.Generic;
using Database.Models.Model.Competition;
using Database.Models.ViewModel.Base;

namespace Database.Models.ViewModel.Competition
{
    public class CompetitionMemberVIewModel : ViewModelBase
    {
        public List<CompetitionModel> CompetitionModels { get; set; }
    }
}