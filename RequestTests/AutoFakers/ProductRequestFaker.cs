using Bogus;
using RequestDomain.DTO;
using RequestDomain.Entities;
using RequestDomain.Entities.Enums;

namespace RequestTests.AutoFakers
{
    public class ProductRequestFaker
    {
        public static ProductRequest GenerateProductRequest()
        {
            Faker<ProductRequest> productRequest = new Faker<ProductRequest>()
                .RuleFor(p => p.Id, Guid.NewGuid())
                .RuleFor(p => p.ProductDescription, p => p.Random.String(1, 256))
                .RuleFor(p => p.ProductCategory, p => p.PickRandom<EnumProductCategory>())
                .RuleFor(p => p.Quantity, p => p.Random.Int(1, 10))
                .RuleFor(p => p.Value, p => p.Random.Decimal(1, 100));

            return productRequest;
        }

        public static ProductRequestDto GenerateProductRequestDto()
        {
            Faker<ProductRequestDto> productRequestDto = new Faker<ProductRequestDto>()
                .RuleFor(p => p.Id, Guid.NewGuid())
                .RuleFor(p => p.ProductDescription, p => p.Random.String(1, 256))
                .RuleFor(p => p.ProductCategory, p => p.PickRandom<EnumProductCategory>())
                .RuleFor(p => p.Quantity, p => p.Random.Int(1, 10))
                .RuleFor(p => p.Value, p => p.Random.Decimal(1, 100));

            return productRequestDto;
        }
    }
}
