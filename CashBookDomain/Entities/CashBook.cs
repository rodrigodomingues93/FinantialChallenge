using CashBookDomain.Entities.Enums;
using Infrastructure.Base;

namespace CashBookDomain.Entities
{
	public class CashBook : EntityBase
	{
		public EnumOrigin Origin { get; set; }
		public Guid? OriginId { get; set; }
		public string Description { get; set; }
		public EnumType Type { get; set; }
		public decimal Value { get; set; }
	}
}
