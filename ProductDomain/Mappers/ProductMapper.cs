using AutoMapper;
using ProductDomain.DTO;
using ProductDomain.Entities;

namespace ProductDomain.Mappers
{
	public class ProductMapper :Profile
	{
		public ProductMapper()
		{
			CreateMap<Product, ProductDto>().ReverseMap();
		}
	}
}
