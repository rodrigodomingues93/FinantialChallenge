using CashBookDomain.Entities.Enums;

namespace CashBookAPIClient.Interfaces
{
	public interface ICashBookClient
	{
		Task<bool> AddCashBook(EnumOrigin origin, Guid id, string description, EnumType type, decimal value);
	}
}
