using Infrastructure.Repository.Interface;
using ProductDomain.Entities;
using System.Linq.Expressions;

namespace ProductData.Repositories.Interfaces
{
	public interface IProductRepository : IGenericRepository<Product>
	{
		Task<List<Product>> GetProductCategoryAsync(Expression<Func<Product, bool>> predicate);
	}
}
