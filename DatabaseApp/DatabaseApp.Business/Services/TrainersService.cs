using System;
using System.Linq;
using Database.DataAccess.Entities.Trainers;
using Database.DataAccess.Repositories.Base.Interfaces;
using Database.Models.Model.Sport;
using Database.Models.Model.SportClub;
using Database.Models.Model.Trainer;
using Database.Models.ViewModel.Trainer;

namespace Database.Business.Services
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