using AutoMapper;
using CashBookDomain.DTO;
using CashBookDomain.Entities;

namespace CashBookDomain.Mappers
{
	public class CashBookMapper : Profile
	{
		public CashBookMapper()
		{
			CreateMap<CashBook, CashBookDto>().ReverseMap();
		}
	}
}
