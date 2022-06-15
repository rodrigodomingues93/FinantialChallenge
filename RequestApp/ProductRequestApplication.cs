using AutoMapper;
using Infrastructure.ErrorMessages;
using Infrastructure.Pagination;
using RequestApp.Interfaces;
using RequestData.Repository.Interfaces;
using RequestDomain.DTO;
using RequestDomain.Entities;
using System.Net;

namespace RequestApp
{
	public class ProductRequestApplication : IProductRequestApplication
	{
		private readonly IProductRequestRepository _productRepository;
		private readonly IMapper _mapper;

		public ProductRequestApplication(IProductRequestRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<ProductRequest>> GetAllProducts()
		{
			var productDto = new ProductRequestDto();
			var products = _productRepository.GetAll().ToList();

			if (products.Count() == 0)
			{
				var error = NotFound(productDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			return products;
		}

		public string ListOfErrors(Error<ProductRequestDto> error)
		{
			var errors = string.Empty;

			foreach (var item in error.Message)
				errors += item.ToString() + " ";

			return errors;
		}
		public Error<ProductRequestDto> NotFound(ProductRequestDto productDto)
		{
			var errors = new List<string>();
			errors.Add("The Product you request was not found in the DataBase!");

			var error = new Error<ProductRequestDto>(HttpStatusCode.NoContent.GetHashCode().ToString(), errors, productDto);
			return error;
		}
		public Error<ProductRequestDto> BadRequest(ProductRequestDto productDto, string message)
		{
			var errors = new List<string>();
			errors.Add(message);

			var error= new Error<ProductRequestDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), errors, productDto);
			return error;
		}
	}
}
