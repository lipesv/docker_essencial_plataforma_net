namespace Domain.Core.Entities
{
    public interface IEntity
    {
        object Id { get; set; }
    }

    public interface IEntity<TId> : IEntity
    {
        new TId Id { get; set; }
    }
}