using ProductCatalog.Domain.Entities.Base;

namespace ProductCatalog.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}