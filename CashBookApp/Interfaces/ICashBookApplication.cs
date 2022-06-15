using CashBookDomain.DTO;
using CashBookDomain.Entities;
using CashBookDomain.Models;
using Infrastructure.Pagination;

namespace CashBookApp.Interfaces
{
	public interface ICashBookApplication
	{
		Task AddCashBook(CashBookDto cashBookDto);
		Task<CashBookModel> GetAllCashBooks(PageParameters page);
		Task<CashBook> GetCashBookById(Guid id);
		Task<CashBook> GetCashBookByOriginId(Guid originId);
		Task<CashBook> UpdateCashBook(Guid id, CashBookDto cashBookDto);
	}
}
