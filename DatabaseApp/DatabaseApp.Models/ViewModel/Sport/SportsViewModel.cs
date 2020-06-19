using System.Collections.Generic;
using Database.Models.Model.Sport;
using Database.Models.ViewModel.Base;

namespace Database.Models.ViewModel.Sport
{
    public class SportsViewModel : ViewModelBase
    {
        public List<SportModel> SportModels { get; set; }
    }
}