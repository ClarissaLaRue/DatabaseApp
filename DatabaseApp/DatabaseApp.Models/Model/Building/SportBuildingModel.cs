using Database.Models.ViewModel.Base;

namespace Database.Models.Model.Building
{
    public class SportBuildingModel : ModelBase
    {
        public string Name { get; set; }
        
        public string Type { get; set; }
        
        public string Address { get; set; }
    }
}