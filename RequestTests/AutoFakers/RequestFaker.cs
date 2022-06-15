using Bogus;
using RequestDomain.DTO;
using RequestDomain.Entities.Enums;

namespace RequestTests.AutoFakers
{
    public class RequestFaker
    {
        public static RequestDto GenerateRequestDto()
        {
            Faker<ProductRequestDto> productRequestDto = new Faker<ProductRequestDto>()
                .RuleFor(p => p.Id, Guid.NewGuid())
                .RuleFor(p => p.ProductDescription, p => p.Random.String(1, 256))
                .RuleFor(p => p.ProductCategory, p => p.PickRandom<EnumProductCategory>())
                .RuleFor(p => p.Quantity, p => p.Random.Int(1, 10))
                .RuleFor(p => p.Value, p => p.Random.Decimal(1, 100));

            RequestDto requestDto = new Faker<RequestDto>()
                .RuleFor(r => r.Code, r => r.Random.Number(1, 9999))
                .RuleFor(r => r.Date, DateTime.UtcNow)
                .RuleFor(r => r.DeliveryDate, DateTime.UtcNow)
                .RuleFor(r => r.Products, r => productRequestDto.GenerateBetween(1, 5))
                .RuleFor(r => r.Client, Guid.NewGuid())
                .RuleFor(r => r.ClientDescription, r => r.Random.String(1, 256))
                .RuleFor(r => r.ClientEmail, r => r.Person.Email)
                .RuleFor(r => r.ClientPhone, r => r.Person.Phone)
                .RuleFor(r => r.Status, r => r.PickRandom<EnumStatus>())
                .RuleFor(r => r.Discount, 0)
                .RuleFor(r => r.Cost, 0)
                .RuleFor(r => r.TotalValue, 0);

            return requestDto;
        }
    }
}
