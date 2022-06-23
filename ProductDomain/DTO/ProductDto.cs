using ProductDomain.Entities.Enums;

namespace ProductDomain.DTO
{
	public class ProductDto
	{
		public string? Code { get; set; }
		public string ProductDescription { get; set; }
		public EnumProductCategory ProductCategory { get; set; }
		public string GTIN { get; set; }
		public string? QRCode { get; set; }
	}
}
