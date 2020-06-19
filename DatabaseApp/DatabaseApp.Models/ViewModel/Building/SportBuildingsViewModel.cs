using System.Collections.Generic;
using Database.Models.Model.Building;
using Database.Models.ViewModel.Base;

namespace Database.Models.ViewModel.Building
{
    public class SportBuildingsViewModel : ViewModelBase
    {
        public List<SportBuildingModel> SportBuildingModels { get; set; }
    }
}