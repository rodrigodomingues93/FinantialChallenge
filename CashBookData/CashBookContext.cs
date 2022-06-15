using CashBookDomain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CashBookData
{
	public class CashBookContext : SqlContext
	{
		public CashBookContext(DbContextOptions<CashBookContext> options) : base(options)
		{

		}

		public DbSet<CashBook> DbCashBook { get; set; }
	}
}
