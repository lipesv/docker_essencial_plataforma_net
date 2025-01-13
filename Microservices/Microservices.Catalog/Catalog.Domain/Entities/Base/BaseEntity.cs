using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Domain.Entities.Base
{
    public abstract class BaseEntity<TKey>
    {
        public abstract TKey Id { get; set; }
    }
}