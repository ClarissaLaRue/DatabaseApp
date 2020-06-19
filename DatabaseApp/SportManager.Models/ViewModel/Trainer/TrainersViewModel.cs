using System.Collections.Generic;
using SportManager.Models.Base;
using SportManager.Models.Model.Trainer;

namespace SportManager.Models.ViewModel.Trainer
{
    public class TrainersViewModel : ViewModelBase
    {
        public List<TrainerModel> Trainers { get; set; }
    }
}