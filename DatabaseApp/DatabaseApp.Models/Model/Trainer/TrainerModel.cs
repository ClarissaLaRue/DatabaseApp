using Database.Models.ViewModel.Base;

namespace Database.Models.Model.Trainer
{
    public class TrainerModel : ModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string SportName { get; set; }
        
        public int SportClubId { get; set; }

        public string SportClubName { get; set; }
    }
    
}