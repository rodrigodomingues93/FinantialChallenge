using CashBookDomain.Entities;

namespace CashBookDomain.Models
{
	public class CashBookModel
	{
		public IEnumerable<CashBook> Models { get; set; }
		public decimal Total {get; set; }
	}
}
