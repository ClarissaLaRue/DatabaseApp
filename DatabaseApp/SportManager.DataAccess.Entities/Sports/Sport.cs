using SportManager.DataAccess.Entities.Base;

namespace SportManager.DataAccess.Entities.Sports
{
    public class Sport : EntityBase<int>
    {
        [NotNull]
        [MaxLength(255)]
        public string SportName { get; set; } 
    }
}