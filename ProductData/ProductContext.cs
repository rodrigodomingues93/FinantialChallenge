using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProductDomain.Entities;

namespace ProductData
{
	public class ProductContext : SqlContext
	{
		public ProductContext(DbContextOptions<ProductContext> options) : base(options)
		{

		}

		public DbSet<Product> DbProducts { get; set; }
	}
}
