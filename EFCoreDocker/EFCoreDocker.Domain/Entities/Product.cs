using EFCoreDocker.Domain.Entities.Base;

namespace EFCoreDocker.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}