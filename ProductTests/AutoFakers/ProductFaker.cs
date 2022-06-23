using Bogus;
using ProductDomain.DTO;
using ProductDomain.Entities;
using ProductDomain.Entities.Enums;

namespace ProductTests.AutoFakers
{
	public class ProductFaker
	{
		public static ProductDto GenerateProductDto()
		{
			ProductDto productDto = new Faker<ProductDto>()
				.RuleFor(p => p.Code, p => p.Random.String(1, 256))
				.RuleFor(p => p.ProductDescription, p => p.Random.String(1, 256))
				.RuleFor(p => p.ProductCategory, p => p.PickRandom<EnumProductCategory>())
				.RuleFor(p => p.GTIN, p => p.Random.String(1, 256))
				.RuleFor(p => p.QRCode, p => p.Random.String(1, 256));

			return productDto;
		}

		public static Product GenerateProduct()
		{
			Product product = new Faker<Product>()
				.RuleFor(p => p.Code, p => p.Random.String(1, 256))
				.RuleFor(p => p.ProductDescription, p => p.Random.String(1, 256))
				.RuleFor(p => p.ProductCategory, p => p.PickRandom<EnumProductCategory>())
				.RuleFor(p => p.GTIN, p => p.Random.String(1, 256))
				.RuleFor(p => p.QRCode, p => p.Random.String(1, 256));

			return product;
		}

		public static List<Product> GenerateProductList()
		{
			    List<Product> products = new Faker<Product>()
				.RuleFor(x => x.Code, x => x.Random.String(1, 256))
				.RuleFor(x => x.ProductDescription, x => x.Random.String(1, 256))
				.RuleFor(x => x.ProductCategory, x => x.PickRandom<EnumProductCategory>())
				.RuleFor(x => x.GTIN, x => x.Random.String(1, 256))
				.RuleFor(x => x.QRCode, x => x.Random.String(1, 256))
				.GenerateBetween(1, 5);

			return products;
		}
	}
}
