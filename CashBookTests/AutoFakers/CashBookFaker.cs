using Bogus;
using CashBookDomain.DTO;
using CashBookDomain.Entities;
using CashBookDomain.Entities.Enums;

namespace CashBookTests.AutoFakers
{
    public class CashBookFaker
    {
        public static CashBookDto GenerateCashBookDto()
        {
            CashBookDto cashBookDto = new Faker<CashBookDto>()
               .RuleFor(c => c.Origin, c => c.PickRandom<EnumOrigin>())
               .RuleFor(c => c.OriginId, Guid.NewGuid())
               .RuleFor(c => c.Description, c => c.Random.String(1, 256))
               .RuleFor(c => c.Type, c => c.PickRandom<EnumType>())
               .RuleFor(c => c.Value, c => c.Random.Decimal(1, 200));

            return cashBookDto;
        }

        public static CashBook GenerateCashBook()
        {
            CashBook cashBook = new Faker<CashBook>()
                .RuleFor(c => c.Id, Guid.NewGuid())
                .RuleFor(c => c.Origin, c => c.PickRandom<EnumOrigin>())
                .RuleFor(c => c.OriginId, Guid.NewGuid())
                .RuleFor(c => c.Description, c => c.Random.String(1, 256))
                .RuleFor(c => c.Type, c => c.PickRandom<EnumType>())
                .RuleFor(c => c.Value, c => c.Random.Decimal(1, 200));

            return cashBook;
        }
    }
}
