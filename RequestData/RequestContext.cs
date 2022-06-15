using Infrastructure;
using Microsoft.EntityFrameworkCore;
using RequestData.Configurations;
using RequestDomain.Entities;

namespace RequestData
{
	public class RequestContext : SqlContext
	{
		public RequestContext(DbContextOptions<RequestContext> options) : base(options)
		{

		}

		public DbSet<Request> DbRequests { get; set; }
		public DbSet<ProductRequest> DbProductRequests { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new RequestConfiguration());
			builder.ApplyConfiguration(new ProductRequestConfiguration());
		}
	}
}
