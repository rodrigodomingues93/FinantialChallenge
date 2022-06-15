using Bogus;
using DocumentDomain.DTO;
using DocumentDomain.Entities;
using DocumentDomain.Entities.Enums;

namespace DocumentTests.AutoFakers
{
    public class DocumentFaker
    {
        public static DocumentDto GenerateDocumentDto()
        {
            DocumentDto documentDto = new Faker<DocumentDto>()
                .RuleFor(d => d.Number, d => d.Random.String(1, 256))
                .RuleFor(d => d.Date, DateTime.UtcNow)
                .RuleFor(d => d.DocumentType, d => d.PickRandom<EnumDocumentType>())
                .RuleFor(d => d.Operation, d => d.PickRandom<EnumOperation>())
                .RuleFor(d => d.Paid, d => d.Random.Bool())
                .RuleFor(d => d.PaymentDate, DateTime.UtcNow)
                .RuleFor(d => d.Description, d => d.Random.String(1, 256))
                .RuleFor(d => d.Total, d => d.Random.Decimal(1, 1000))
                .RuleFor(d => d.Observations, d => d.Random.String(1, 256));

            return documentDto;
        }

        public static Document GenerateDocument()
        {
            Document document = new Faker<Document>()
                 .RuleFor(d => d.Id, Guid.NewGuid())
                 .RuleFor(d => d.Number, d => d.Random.String(1, 256))
                 .RuleFor(d => d.Date, DateTime.UtcNow)
                 .RuleFor(d => d.DocumentType, d => d.PickRandom<EnumDocumentType>())
                 .RuleFor(d => d.Operation, d => d.PickRandom<EnumOperation>())
                 .RuleFor(d => d.Paid, d => d.Random.Bool())
                 .RuleFor(d => d.PaymentDate, DateTime.UtcNow)
                 .RuleFor(d => d.Description, d => d.Random.String(1, 256))
                 .RuleFor(d => d.Total, d => d.Random.Decimal(1, 1000))
                 .RuleFor(d => d.Observations, d => d.Random.String(1, 256));

            return document;
        }
    }
}
