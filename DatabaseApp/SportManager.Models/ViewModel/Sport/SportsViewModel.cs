using System.Collections.Generic;
using SportManager.Models.Base;
using SportManager.Models.Model.Sport;

namespace SportManager.Models.ViewModel.Sport
{
    public class SportsViewModel : ViewModelBase
    {
        public List<SportModel> SportModels { get; set; }
    }
}