using System.Collections.Generic;
using SportManager.Models.Base;
using SportManager.Models.Model.SportMan;

namespace SportManager.Models.ViewModel.SportMan
{
    public class SportManViewModel : ViewModelBase
    {
        public List<SportManModel> SportManModels { get; set; }
    }
}