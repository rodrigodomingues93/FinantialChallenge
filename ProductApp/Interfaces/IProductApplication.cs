using Infrastructure.Pagination;
using ProductDomain.DTO;
using ProductDomain.Entities;
using ProductDomain.Entities.Enums;

namespace ProductApp.Interfaces
{
	public interface IProductApplication
	{
		Task AddProduct(ProductDto productDto);
		Task<IEnumerable<Product>> GetAllProducts(PageParameters page);
		Task<Product> GetProductById(Guid id);
		Task<IEnumerable<Product>> GetProductByCategory(EnumProductCategory category);
		Task<Product> UpdateProduct(Guid id, ProductDto productDto);
		Task<Product> DeleteProduct(Guid id);
	}
}
