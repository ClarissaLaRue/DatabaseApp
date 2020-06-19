using System.Collections.Generic;
using Database.Models.Model.SportClub;
using Database.Models.ViewModel.Base;

namespace Database.Models.ViewModel.SportClub
{
    public class SportClubViewModel : ViewModelBase
    {
        public List<SportClubModel> SportClubModels { get; set; }
    }
}