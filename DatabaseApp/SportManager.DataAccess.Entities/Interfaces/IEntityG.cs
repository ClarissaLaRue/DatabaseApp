namespace SportManager.DataAccess.Entities.Interfaces
{
    public interface IEntity<out TId> : IEntity
    {
        TId ID { get; }   
    }
}