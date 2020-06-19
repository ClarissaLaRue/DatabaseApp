using System.Collections.Generic;
using Database.Models.Model.Competition;
using Database.Models.ViewModel.Base;

namespace Database.Models.ViewModel.Sport
{
    public class CompetitionViewModel : ViewModelBase
    {
        public List<CompetitionModel> CompetitionModels { get; set; }
    }
}