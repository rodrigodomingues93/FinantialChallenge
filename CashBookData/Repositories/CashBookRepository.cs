using CashBookData.Repositories.Interfaces;
using CashBookDomain.Entities;
using Infrastructure.Pagination;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace CashBookData.Repositories
{
	public class CashBookRepository : GenericRepository<CashBook>, ICashBookRepository
	{
		private readonly CashBookContext _context;

		public CashBookRepository(CashBookContext context) : base(context)
		{
			_context = context;
		}
	}
}
