using System.Collections.Generic;
using SportManager.Models.Base;
using SportManager.Models.Model.SportClub;

namespace Database.Models.ViewModel.SportClub
{
    public class SportClubViewModel : ViewModelBase
    {
        public List<SportClubModel> SportClubModels { get; set; }
    }
}