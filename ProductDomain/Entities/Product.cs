using Infrastructure.Base;
using ProductDomain.Entities.Enums;

namespace ProductDomain.Entities
{
	public class Product : EntityBase
	{
		public string? Code { get; set; }
		public string ProductDescription { get; set; }
		public EnumProductCategory ProductCategory { get; set; }
		public string GTIN { get; set; }
		public string? QRCode { get; set; }
	}
}
