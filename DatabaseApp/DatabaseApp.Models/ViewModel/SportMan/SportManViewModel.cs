using System.Collections.Generic;
using Database.Models.Model.SportMan;
using Database.Models.ViewModel.Base;

namespace Database.Models.ViewModel.SportMan
{
    public class SportManViewModel : ViewModelBase
    {
        public List<SportManModel> SportManModels { get; set; }
    }
}