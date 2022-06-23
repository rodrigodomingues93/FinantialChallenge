using Infrastructure.Pagination;
using Moq;
using Moq.AutoMock;
using ProductAPI.Controllers;
using ProductApp.Interfaces;
using ProductDomain.DTO;
using ProductTests.AutoFakers;
using Xunit;

namespace ProductTests
{
	public class ProductTest
	{
		private readonly AutoMocker _mocker;
		public ProductTest()
		{
			_mocker = new AutoMocker();
		}

		[Fact(DisplayName = "AddProduct Test")]
		public async Task AddProductTest()
		{
			var productDto = ProductFaker.GenerateProductDto();

			var productService = _mocker.GetMock<IProductApplication>();
			productService.Setup(p => p.AddProduct(productDto));

			var productController = _mocker.CreateInstance<ProductController>();

			await productController.AddProduct(productDto);

			productService.Verify(p => p.AddProduct(It.IsAny<ProductDto>()), Times.Once());
		}
		
		[Fact(DisplayName = "GetAllProducts Test")]
		public async Task GetAllProductsTest()
		{
			var productService = _mocker.GetMock<IProductApplication>();
			productService.Setup(p => p.GetAllProducts(null));

			var productController = _mocker.CreateInstance<ProductController>();
			var page = new PageParameters();

			await productController.GetAllProducts(page);

			productService.Verify(p => p.GetAllProducts(page), Times.Once());

		}

		[Fact(DisplayName = "GetProductById Test")]
		public async Task GetProductByIdTest()
		{
			var product = ProductFaker.GenerateProduct();

			var productService = _mocker.GetMock<IProductApplication>();
			productService.Setup(p => p.GetProductById(product.Id));

			var productController = _mocker.CreateInstance<ProductController>();

			await productController.GetProductById(product.Id);

			productService.Verify(p => p.GetProductById(product.Id), Times.Once());
		}

		[Fact(DisplayName = "GetProductByCategory Test")]
		public async Task GetProductByCategoryTest()
		{
			var product = ProductFaker.GenerateProduct();

			var productService = _mocker.GetMock<IProductApplication>();
			productService.Setup(p => p.GetProductByCategory(product.ProductCategory));

			var productController = _mocker.CreateInstance<ProductController>();

			await productController.GetProductByCategory(product.ProductCategory);

			productService.Verify(p => p.GetProductByCategory(product.ProductCategory), Times.Once());

		}

		[Fact(DisplayName = "UpdateProduct Test")]
		public async Task UpdateProductTest()
		{
			var product = ProductFaker.GenerateProduct();
			var productDto = ProductFaker.GenerateProductDto();

			var productService = _mocker.GetMock<IProductApplication>();
			productService.Setup(p => p.GetProductById(product.Id));
			productService.Setup(p => p.UpdateProduct(product.Id, productDto));

			var productController = _mocker.CreateInstance<ProductController>();

			await productController.UpdateProduct(product.Id, productDto);

			productService.Verify(p => p.UpdateProduct(product.Id, It.IsAny<ProductDto>()), Times.Once());
		}
		
		[Fact(DisplayName = "DeleteProduct Test")]
		public async Task DeleteProductTest()
		{
			var product = ProductFaker.GenerateProduct();

			var productService= _mocker.GetMock<IProductApplication>();
			productService.Setup(p => p.DeleteProduct(product.Id));

			var productController = _mocker.CreateInstance<ProductController>();

			await productController.DeleteProduct(product.Id);

			productService.Verify(p => p.DeleteProduct(product.Id), Times.Once());
		}
	}
}