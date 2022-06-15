using AutoMapper;
using DocumentDomain.DTO;
using DocumentDomain.Entities;

namespace DocumentDomain.Mappers
{
	public class DocumentMapper : Profile
	{
		public DocumentMapper()
		{
			CreateMap<Document, DocumentDto>().ReverseMap();
		}
	}
}
