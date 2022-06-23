using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using ProductData.Repositories.Interfaces;
using ProductDomain.Entities;
using System.Linq.Expressions;

namespace ProductData.Repositories
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		public ProductRepository(ProductContext context) : base(context)
		{

		}
		public async Task<List<Product>> GetProductCategoryAsync(Expression<Func<Product, bool>> predicate)
		{
			var stack= _dbSet.Where(predicate);

			if (_include != null)
				stack = _include(stack);

			var result = await stack.AsNoTracking().ToListAsync();

			return result;
		}
	}
}
