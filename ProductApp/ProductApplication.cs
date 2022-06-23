using AutoMapper;
using Infrastructure.ErrorMessages;
using Infrastructure.Pagination;
using ProductApp.Interfaces;
using ProductData.Repositories.Interfaces;
using ProductDomain.DTO;
using ProductDomain.Entities;
using ProductDomain.Entities.Enums;
using ProductDomain.Validators;
using System.Net;

namespace ProductApp
{
	public class ProductApplication : IProductApplication
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;

		public ProductApplication(IProductRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public async Task AddProduct(ProductDto productDto)
		{
			var productMap= _mapper.Map<Product>(productDto);

			var product = _productRepository.GetAsync(p => p.GTIN == productDto.GTIN || p.ProductDescription == productDto.ProductDescription);
			if(product.Result != null)
			{
				var error = BadRequest(productDto, "This Description or GTIN is already been used in a product");
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			var validator = new ProductValidator();
			var result = validator.Validate(productMap);
			if (!result.IsValid)
			{
				var error = new Error<ProductDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
						result.Errors.ConvertAll(p => p.ErrorMessage.ToString()), productDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			await _productRepository.AddAsync(productMap);
		}
		public async Task<Product> DeleteProduct(Guid id)
		{
			var product = await _productRepository.GetByIdAsync(id);
			var productDto = _mapper.Map<ProductDto>(product);

			if (product == null)
			{
				var error = NotFound(productDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			await _productRepository.DeleteAsync(product);
			return product;
		}
		public async Task<IEnumerable<Product>> GetAllProducts(PageParameters page)
		{
			var productDto = new ProductDto();
			var products = await _productRepository.GetAllWithPaging(page);

			if (products.Count() == 0)
			{
				var error = NotFound(productDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			return products;
		}
		public async Task<IEnumerable<Product>> GetProductByCategory(EnumProductCategory category)
		{
			var productDto = new ProductDto();
			var product = await _productRepository.GetProductCategoryAsync(p => p.ProductCategory == category);

			if (product == null)
			{
				var error = NotFound(productDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			return product;
		}
		public async Task<Product> GetProductById(Guid id)
		{
			var productDto = new ProductDto();
			var product = await _productRepository.GetByIdAsync(id);

			if (product == null)
			{
				var error = NotFound(productDto);
				var errors = ListOfErrors(error);
				throw new Exception(errors);
			}

			return product;
		}
		public async Task<Product> UpdateProduct(Guid id, ProductDto productDto)
		{
			var product = await _productRepository.GetByIdAsync(id);

			if(product == null)
			{
				var error1 = NotFound(productDto);
				var errors1 = ListOfErrors(error1);
				throw new Exception(errors1);
			}

			var productMap = _mapper.Map<Product>(productDto);
			productMap.Id = id;

			var products = _productRepository.GetAsync(p => p.GTIN == productDto.GTIN || p.ProductDescription == productDto.ProductDescription);
			if (products.Result != null)
			{
				var error2 = BadRequest(productDto, "This Description or GTIN is already been used in a product");
				var errors2 = ListOfErrors(error2);
				throw new Exception(errors2);
			}

			var validator = new ProductValidator();
			var result = validator.Validate(productMap);
			if (!result.IsValid)
			{
				var error3 = new Error<ProductDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
						result.Errors.ConvertAll(p => p.ErrorMessage.ToString()), productDto);
				var errors3 = ListOfErrors(error3);
				throw new Exception(errors3);
			}

			await _productRepository.UpdateAsync(productMap);
			return productMap;
		}

		public string ListOfErrors(Error<ProductDto> error)
		{
			var errors = string.Empty;

			foreach (var item in error.Message)
				errors += item.ToString() + " ";

			return errors;
		}
		public Error<ProductDto> NotFound(ProductDto productDto)
		{
			var errors = new List<string>();
			errors.Add("The Product you request was not found in the DataBase!");

			var error = new Error<ProductDto>(HttpStatusCode.NoContent.GetHashCode().ToString(), errors, productDto);
			return error;
		}
		public Error<ProductDto> BadRequest(ProductDto productDto, string message)
		{
			var errors = new List<string>();
			errors.Add(message);

			var error = new Error<ProductDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), errors, productDto);
			return error;
		}

	}
}
