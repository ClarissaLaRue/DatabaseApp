using System.Collections.Generic;
using SportManager.Models.Base;
using SportManager.Models.Model.Competition;

namespace SportManager.Models.ViewModel.Competition
{
    public class CompetitionViewModel : ViewModelBase
    {
        public List<CompetitionModel> CompetitionModels { get; set; }
    }
}