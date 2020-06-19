namespace SportManager.DataAccess.Entities.Interfaces
{
    public interface IModifiable : IModel
    {
        bool IsDirty { get; set; }
    }
}