using AutoMapper;
using RequestDomain.DTO;
using RequestDomain.Entities;

namespace RequestDomain.Mappers
{
	public class RequestMapper : Profile
	{
		public RequestMapper()
		{
			CreateMap<Request, RequestDto>().ReverseMap();
			//CreateMap<RequestModel, Request>().ReverseMap();
			//CreateMap<RequestDto, RequestModel>().ReverseMap();
			CreateMap<ProductRequest, ProductRequestDto>().ReverseMap();
		}
	}
}
