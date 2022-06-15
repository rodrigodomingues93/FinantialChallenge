using RequestDomain.Entities.Enums;

namespace RequestDomain.DTO
{
    public class ProductRequestDto
    {
        public Guid Id { get; set; }
        //public Guid ProductId { get; set; }
        public string ProductDescription { get; set; }
        public EnumProductCategory ProductCategory { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value { get; set; }
        public decimal Total => (Value * Quantity);
    }
}
