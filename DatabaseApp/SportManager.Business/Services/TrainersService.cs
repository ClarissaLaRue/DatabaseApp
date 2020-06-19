using System;
using System.Linq;
using Database.DataAccess.Entities.Trainers;
using SportManager.DataAccess.Repositories.Base.Interfaces;
using SportManager.Models.Model.Trainer;
using SportManager.Models.ViewModel.Trainer;

namespace SportManager.Business.Services
{
    public class TrainersService
    {
        private readonly ISqlRepository<Trainer> _trainersRepository;

        public TrainersService(ISqlRepository<Trainer> trainersRepository)
        {
            _trainersRepository = trainersRepository ?? throw new ArgumentNullException(nameof(trainersRepository));
        }

        public TrainersViewModel GetTrainersViewModel()
        {
            var trainers = _trainersRepository.Select(x=>new TrainerModel
            {
                Id = x.ID,
                Name = x.Name,
                SportName = x.Sport.SportName,
                SportClubId = x.SportClubID,
                SportClubName = x.SportClub.Name
            }).ToList();

            var result = new TrainersViewModel
            {
                Trainers = trainers
            };

            return result;
        }
        
    }
}