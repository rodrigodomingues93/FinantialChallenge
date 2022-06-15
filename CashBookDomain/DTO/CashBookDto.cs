using CashBookDomain.Entities.Enums;

namespace CashBookDomain.DTO
{
	public class CashBookDto
	{
		public EnumOrigin Origin { get; set; }
		public Guid? OriginId { get; set; }
		public string Description { get; set; }
		public EnumType Type { get; set; }
		public decimal Value { get; set; }
	}
}
