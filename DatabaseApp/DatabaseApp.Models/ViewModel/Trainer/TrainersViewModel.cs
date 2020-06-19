using System.Collections;
using System.Collections.Generic;
using Database.Models.Model.Trainer;
using Database.Models.ViewModel.Base;

namespace Database.Models.ViewModel.Trainer
{
    public class TrainersViewModel : ViewModelBase
    {
        public List<TrainerModel> Trainers { get; set; }
    }
}