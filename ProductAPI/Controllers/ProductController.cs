using Infrastructure.ErrorMessages;
using Infrastructure.Pagination;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Interfaces;
using ProductDomain.DTO;
using ProductDomain.Entities.Enums;
using System.Net;

namespace ProductAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductApplication _productApplication;
		public ProductController(IProductApplication productApplication)
		{
			_productApplication = productApplication;
		}

		[HttpPost]
		public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
		{
			try
			{
				await _productApplication.AddProduct(productDto);
				return Ok(productDto);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<ProductDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, productDto));
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProducts([FromQuery] PageParameters page)
		{
			try
			{
				var products = await _productApplication.GetAllProducts(page);
				return Ok(products);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<ProductDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, new ProductDto()));
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(Guid id)
		{
			try
			{
				var product = await _productApplication.GetProductById(id);
				return Ok(product);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<ProductDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, new ProductDto()));
			}
		}

		[HttpGet("{category}")]
		public async Task<IActionResult> GetProductByCategory(EnumProductCategory category)
		{
			try
			{
				var products = await _productApplication.GetProductByCategory(category);
				return Ok(products);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<ProductDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, new ProductDto()));
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductDto productDto)
		{
			try
			{
				var product = await _productApplication.UpdateProduct(id, productDto);
				return Ok(product);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<ProductDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, productDto));
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(Guid id)
		{
			try
			{
				var product = await _productApplication.DeleteProduct(id);
				return Ok(product);
			}
			catch (Exception fail)
			{
				var listOfErrors = new List<string>();
				listOfErrors.Add(fail.Message);
				return StatusCode((int)HttpStatusCode.BadRequest, new Error<ProductDto>(HttpStatusCode.BadRequest.GetHashCode().ToString(), listOfErrors, null));
			}
		}
	}
}
