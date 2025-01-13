namespace Catalog.Domain.Entities.Base
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}